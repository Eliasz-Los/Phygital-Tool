using Microsoft.EntityFrameworkCore;
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

    public async Task<Post> ReadPostWithAccountAndWithThemeById(long id)
    {
        return await _dbContext.Posts.Include(p => p.Account)
            .Include(p => p.Theme)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    private Post ReadPostById(long id)
    {
        return _dbContext.Posts.Find(id);
    }

    public async Task<IEnumerable<Post>> ReadAllPostsWithAccountWithThemeAndWithReactionsAndLikesOrderByDescPostTime()
    {
        var result = await _dbContext.Posts
            .Include(p => p.Account)
            .Include(p => p.Theme)
            .Include(p => p.PostReactions)
            .Include(p => p.PostLikes)
            .ThenInclude(pl => pl.Like)
            .OrderByDescending(p => p.PostTime)
            .ToListAsync();
        return result;
    }

    public async Task CreatePost(Post post)
    {
        post = await UploadFile(post);
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Post> UploadFile(Post post)
    {
        string fileNameForStorage =  $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}-{new Random().Next()}.{Path.GetExtension(post.ImageFile.FileName)}";
        post.ImageUrl = await _cloudStorageService.UploadFileToBucket(post.ImageFile, fileNameForStorage);
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
    
    public async Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account)
    {
        var post = await ReadPostByIdAsync(postId);

        reaction.Account = account;
        var postReaction = new PostReaction { Post = post, Reaction = reaction };
        _dbContext.PostReactions.Add(postReaction);
        return reaction;
    }

    public async Task<PostLike> ReadDislikeByPostIdAndAccountId(long postId, string currentAccountId)
    {
        return await _dbContext.PostLikes.Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == currentAccountId)
            .FirstOrDefaultAsync();
    }

    public async Task DeletePostDislikeByPostIdAndAccountId(long postId, string currenAccountId)
    {
        var dislike = await ReadDislikeByPostIdAndAccountId(postId, currenAccountId);
        if (dislike != null)
        {
            _dbContext.PostLikes.Remove(dislike);
        }
    }

    public async Task<PostLike> ReadLikeByPostIdAndAccountId(long postId, string currentAccountId)
    {
        return await _dbContext.PostLikes.Where(pl => pl.Post.Id == postId && pl.Like.Account.Id == currentAccountId)
            .FirstOrDefaultAsync();
    }
    
    public async Task DeletePostLikeByPostIdAndAccountId(long postId, string currentAccountId)
    {
        var like = await ReadLikeByPostIdAndAccountId(postId, currentAccountId);
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

    public async Task<IEnumerable<PostReaction>> ReadReactionsWithAccountAndLikesOfPostByPostId(long postId)
    {
        return await _dbContext.PostReactions
            .Include(pr => pr.Reaction)
              .ThenInclude(r => r.Account)
            .Include(pr => pr.Reaction)
                .ThenInclude(r => r.ReactionLikes)
                     .ThenInclude(rl => rl.Like)
            .Where(pr => pr.Post.Id == postId)
            .ToListAsync();
    }

    public async Task DeleteReactionToPostByPostIdAndReactionId(long postId, long reactionId)
    {
        var deleteReaction =  await _dbContext.PostReactions
            .Where(pr => pr.Post.Id == postId && pr.Reaction.Id == reactionId)
            .Select(pr => pr.Reaction)
            .FirstOrDefaultAsync();
         _dbContext.Reactions.Remove(deleteReaction);
    }

    public async Task<Reaction> ReadReactionWithAccountById(long reactionId)
    {
        return await _dbContext.Reactions.Include(r => r.Account)
            .Where(r => r.Id == reactionId)
            .FirstOrDefaultAsync();
    }

    public async Task<ReactionLike> CreateReactionLikeByReactionIdAndAccount(long reactionId, Account currentAccount)
    {
        var reaction = await _dbContext.Reactions.FindAsync(reactionId);
        
        var existingLike = await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccount.Id)
            .Select(rl => rl.Like)
            .FirstOrDefaultAsync();

        if (existingLike != null)
        {
            if(existingLike.LikeType == LikeType.ThumbsUp)
            {
                return null;
            }
            else
            {
                _dbContext.Likes.Remove(existingLike);
            }
        }
        
        var like = new Like { LikeType = LikeType.ThumbsUp, Account = currentAccount };
        var reactionLike = new ReactionLike { Like = like, Reaction = reaction };
         _dbContext.ReactionLikes.Add(reactionLike);
         return reactionLike;
    }

    public async Task<ReactionLike> CreateReactionDisLikeByReactionIdAndAccount(long reactionId, Account currentAccount)
    {
        var reaction = await _dbContext.Reactions.FindAsync(reactionId);
        
        var existingLike = await _dbContext.ReactionLikes
            .Where(rl => rl.Reaction.Id == reactionId && rl.Like.Account.Id == currentAccount.Id)
            .Select(rl => rl.Like)
            .FirstOrDefaultAsync();

        if (existingLike != null)
        {
            if (existingLike.LikeType == LikeType.ThumbsDown)
            {
                return null;
            }else
            {
                _dbContext.Likes.Remove(existingLike);
            }
        }
        
        var like = new Like { LikeType = LikeType.ThumbsDown, Account = currentAccount };
        var reactionLike = new ReactionLike { Like = like, Reaction = reaction };
        _dbContext.ReactionLikes.Add(reactionLike);
        return reactionLike;
    }

    public async Task<int> ReadLikesCountByReactionId(long reactionId)
    {
        return await _dbContext.ReactionLikes.CountAsync(l => l.Like.LikeType == LikeType.ThumbsUp && l.Reaction.Id == reactionId);
    }

    public async Task<int> ReadDislikesCountByReactionId(long reactionId)
    {
        return await _dbContext.ReactionLikes.CountAsync(l => l.Like.LikeType == LikeType.ThumbsDown && l.Reaction.Id == reactionId);
    }
    
}