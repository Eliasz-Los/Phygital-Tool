using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Phygital.Domain;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;
//TODO: naming convention
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
    public async Task<Post> ReadPostWithAccountAndWithThemeById(long id)
    {
        return await _dbContext.Posts.Include(p => p.Account)
            .Include(p => p.Theme)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Post ReadPostById(long id)
    {
        return _dbContext.Posts.Find(id);
    }

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

    //TODO; has to go bye bye
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
        /*return await _dbContext.PostReactions
            .Include(pr => pr.Reaction)
            .ThenInclude(r => r.Account)
            .Where(pr => pr.Post.Id == postId)
            .ToListAsync();*/
        
        return await _dbContext.PostReactions
            .Include(pr => pr.Reaction)
              .ThenInclude(r => r.Account)
            .Include(pr => pr.Reaction)
                .ThenInclude(r => r.ReactionLikes)
                     .ThenInclude(rl => rl.Like)
            .Where(pr => pr.Post.Id == postId)
            .ToListAsync();
    }

    public async Task DeleteReactionToPostById(long postId, long reactionId)
    {
        var deleteReaction = await _dbContext.PostReactions
            .Include( pr => pr.Reaction)
            .Where(pr => pr.Post.Id == postId && pr.Reaction.Id == reactionId)
            .FirstOrDefaultAsync();
         _dbContext.PostReactions.Remove(deleteReaction);
    }

    public async Task<Reaction> ReadReactionWithAccountById(long reactionId)
    {
        return await _dbContext.Reactions.Include(r => r.Account)
            .Where(r => r.Id == reactionId)
            .FirstOrDefaultAsync();
    }

    public async Task<ReactionLike> CreateReactionLikeByReactionId(long reactionId, Account currentAccount)
    {
        var reaction = await _dbContext.Reactions.FindAsync(reactionId);
        
        var existingLike = await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccount.Id)
            .FirstOrDefaultAsync();

        if (existingLike != null)
        {
            return null;
        }
        
        var like = new Like { LikeType = LikeType.ThumbsUp, Account = currentAccount };
        var reactionLike = new ReactionLike { Like = like, Reaction = reaction };
         _dbContext.ReactionLikes.Add(reactionLike);
         return reactionLike;
    }

    public async Task<ReactionLike> CreateReactionDisLikeByReactionId(long reactionId, Account currentAccount)
    {
        var reaction = await _dbContext.Reactions.FindAsync(reactionId);
        
        var existingLike = await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccount.Id)
            .FirstOrDefaultAsync();

        if (existingLike != null)
        {
            return null;
        }
        
        var like = new Like { LikeType = LikeType.ThumbsDown, Account = currentAccount };
        var reactionLike = new ReactionLike { Like = like, Reaction = reaction };
        _dbContext.ReactionLikes.Add(reactionLike);
        return reactionLike;
    }

    public async Task<int> ReadLikesCountByReactionId(long reactionId)
    {
        return await _dbContext.ReactionLikes.Where(l => l.Like.LikeType == LikeType.ThumbsUp && l.Reaction.Id == reactionId)
            .CountAsync();
    }

    public async Task<int> ReadDislikesCountByReactionId(long reactionId)
    {
        return await _dbContext.ReactionLikes.Where(l => l.Like.LikeType == LikeType.ThumbsDown && l.Reaction.Id == reactionId)
            .CountAsync();
    }

    public async Task<ReactionLike> ReadDislikeByReactionIdAndUserId(long reactionId, string currentAccountId)
    {
       return await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccountId 
            && rl.Like.LikeType == LikeType.ThumbsDown)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteReactionDislikeByReactionIdAndUserId(long reactionId, string currentAccountId)
    {
        var dislike = await ReadDislikeByReactionIdAndUserId(reactionId, currentAccountId);
        if (dislike != null)
        {
            _dbContext.ReactionLikes.Remove(dislike);
        }
    }

    public async Task<ReactionLike> ReadLikeByReactionIdAndUserId(long reactionId, string currentAccountId)
    {
        return await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccountId
            && rl.Like.LikeType == LikeType.ThumbsUp)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteReactionLikeByReactionIdAndUserId(long reactionId, string currentAccountId)
    {
        var like = await ReadLikeByReactionIdAndUserId(reactionId, currentAccountId);
        if (like != null)
        {
            _dbContext.ReactionLikes.Remove(like);
        }
    }
}