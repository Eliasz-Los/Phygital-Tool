using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Phygital.Domain;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly PhygitalDbContext _dbContext;
    private readonly ICloudStorage _cloudStorageService;
    public FeedbackRepository(PhygitalDbContext dbContext, ICloudStorage cloudStorageService)
    {
        _dbContext = dbContext;
        _cloudStorageService = cloudStorageService;
    }


    public async Task<Post> ReadPostByIdAsync(long id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }

    //todo: naming convention
    public async Task<Post> ReadPostWithThemeByIdAsync(long id)
    {
        return await _dbContext.Posts.Include(p => p.Account)
            .Include(p => p.Theme)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Post ReadPostById(long id)
    {
        return _dbContext.Posts.Find(id);
    }

    //TODO : too big method call, we gaan het opsplitsen nog in 2 delen: Posts en Likes
    public async Task<IEnumerable<Post>> ReadAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes()
    {
        var result = await _dbContext.Posts
            .Include(p => p.Account)
            .Include(p => p.Theme)
            .Include(p => p.PostReactions)
            .Include(p => p.PostLikes)
            .ThenInclude(pl => pl.Like)
            .ToListAsync();
        return result;
    }

    public async Task CreatePost(Post post)
    {
        post = await UploadFile(post);
        Console.WriteLine($"post in CREATE mode: {post.Title}, {post.Text}, {post.ImageUrl}, {post.PostTime}, {post.Theme}, {post.Account}");
        _dbContext.Posts.Attach(post);
        _dbContext.Posts.Add(post);
        try
        {
            await _dbContext.SaveChangesAsync();

        }catch (Exception e)
        {
            Console.WriteLine($"Excepstion while saving new post: {e}");
        }

    }

    private async Task<Post> UploadFile(Post post)
    {
        string fileNameForStorage =  $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}-{new Random().Next()}.{Path.GetExtension(post.ImageFile.FileName)}";
        post.ImageUrl = await _cloudStorageService.UploadFileToBucket(post.ImageFile, fileNameForStorage);
        Console.WriteLine($"image url: {post.ImageUrl}");
        Console.WriteLine($"post in UPLOAD mode: {post.Title}" );
        return post;
    }
    public void UpdatePost(Post post)
    {
        var postToUpdate = ReadPostById(post.Id);

        postToUpdate.Title = post.Title;
        postToUpdate.Text = post.Text;

        _dbContext.Posts.Update(postToUpdate);

    }

    public void DeletePost(long id)
    {
        var removePost = ReadPostById(id);
        _dbContext.Posts.Remove(removePost);
    }

    public async Task<PostLike> LikePost(long postId, Account account)
    {
        var post = await ReadPostByIdAsync(postId);

        var existingLike = await _dbContext.PostLikes
            .Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == account.Id)
            .FirstOrDefaultAsync();

        if (existingLike != null)
        {
            return null;
        }

        var like = new Like { LikeType = LikeType.ThumbsUp, Account = account };
        var postLike = new PostLike { Like = like, Post = post };

        _dbContext.PostLikes.Add(postLike);

        return postLike;
    }

    public async Task<PostLike> DislikePost(long postId, Account account)
    {
        var post = await ReadPostByIdAsync(postId);

        var existingDislike = await _dbContext.PostLikes
            .Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == account.Id)
            .FirstOrDefaultAsync();

        if (existingDislike != null)
        {
            return null;
        }

        var like = new Like { LikeType = LikeType.ThumbsDown, Account = account };
        var postLike = new PostLike { Like = like, Post = post };

        _dbContext.PostLikes.Add(postLike);

        return postLike;
    }

    public async Task<PostLike> DeletePostLike(long postId, long likeId)
    {
        var postLike = await _dbContext.PostLikes
            .Where(pl => pl.Post.Id == postId && pl.Like.Id == likeId)
            .FirstOrDefaultAsync();

        _dbContext.PostLikes.Remove(postLike);

        return postLike;
    }

    public async Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account)
    {
        var post = await ReadPostByIdAsync(postId);

        reaction.Account = account;
        var postReaction = new PostReaction { Post = post, Reaction = reaction };
        _dbContext.PostReactions.Add(postReaction);
        return reaction;
    }

    public async Task<PostLike> ReadDislikeByPostIdAndUserId(long postId, string currentAccountId)
    {
        return await _dbContext.PostLikes.Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == currentAccountId)
            .FirstOrDefaultAsync();
    }

    public async Task DeletePostDislikeByPostId(long postId, string id)
    {
        var dislike = await ReadDislikeByPostIdAndUserId(postId, id);
        if (dislike != null)
        {
            _dbContext.PostLikes.Remove(dislike);
        }
    }

    public async Task<PostLike> ReadLikeByPostIdAndUserId(long postId, string currentAccountId)
    {
        return await _dbContext.PostLikes.Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == currentAccountId)
            .FirstOrDefaultAsync();
    }

    public async Task DeletePostLikeByPostId(long postId, string id)
    {
        var like = await ReadLikeByPostIdAndUserId(postId, id);
        if (like != null)
        {
            _dbContext.PostLikes.Remove(like);
        }
    }

    public async Task<int> ReadLikesCountByPostId(long postId)
    {
        return await _dbContext.PostLikes.Where(l => l.Like.LikeType == LikeType.ThumbsUp && l.Post.Id == postId)
            .CountAsync();
    }

    public async Task<int> ReadDislikesCountByPostId(long postId)
    {
        return await _dbContext.PostLikes.Where(l => l.Like.LikeType == LikeType.ThumbsDown && l.Post.Id == postId)
            .CountAsync();
    }

    public async Task<IEnumerable<PostReaction>> ReadReactionsOfPostByPostId(long postId)
    {
        return await _dbContext.PostReactions
            .Include(pr => pr.Reaction)
            .ThenInclude(r => r.Account)
            .Where(pr => pr.Post.Id == postId)
            .ToListAsync();
    }
}