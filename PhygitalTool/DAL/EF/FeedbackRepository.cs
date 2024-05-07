using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Feedback;

namespace Phygital.DAL.EF;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FeedbackRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Post> ReadPostById(long id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }

    public async Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes()
    {
        var result = await _dbContext.Posts
            .Include(p => p.PostReactions)
            .ThenInclude(pr => pr.Reaction)
            .Include(p => p.PostLikes)
            .ThenInclude(pl => pl.Like)
            .ToListAsync();
        return  result;
    }

    public void CreatePost(Post post)
    {
        _dbContext.Posts.Add(post);
    }

    public async Task<PostLike> LikePost(long postId)
    {
        var post = await ReadPostById(postId);
        
        if (post == null)
        {
            throw new Exception($"Post with id {postId} not found");
        }
        
        var like = new Like{ LikeType = LikeType.ThumbsUp};
        var postLike = new PostLike{ Like = like, Post = post};
       
        //post.PostLikes.Add(postLike);
        
        _dbContext.PostLikes.Add(postLike);
        
       // await _dbContext.SaveChangesAsync();
        
        return postLike;
    }
    
    public async Task<PostLike> DeletePostLike(long postId, long likeId)
    {
       var postLike = await _dbContext.PostLikes
           .Where(pl => pl.Post.Id == postId && pl.Like.Id == likeId)
           .FirstOrDefaultAsync();
       
       _dbContext.PostLikes.Remove(postLike);
       
       // await _dbContext.SaveChangesAsync();

       return postLike;
    }

    public async Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction)
    {
        var post = await ReadPostById(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        var postReaction = new PostReaction{ Post = post, Reaction = reaction};
         _dbContext.PostReactions.Add(postReaction);
        return reaction;
    }
}