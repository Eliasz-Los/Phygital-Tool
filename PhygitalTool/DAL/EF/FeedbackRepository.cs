using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL.EF;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FeedbackRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Post> ReadPostByIdAsync(long id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }
    
    public async Task<Post> ReadPostWithThemeByIdAsync(long id)
    {
        return await _dbContext.Posts.Include(p =>p.Theme).FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public Post ReadPostById(long id)
    {
        return  _dbContext.Posts.Find(id);
    }

    //TODO: better naming and follow naming conventions
    public async Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes()
    {
        var result = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Account)
            .Include(p => p.Theme)
            .Include(p => p.PostReactions)
            .ThenInclude(pr => pr.Reaction)
            .ThenInclude(r => r.Account)
            .Include(p => p.PostLikes)
            .ThenInclude(pl => pl.Like)
            .ThenInclude(l => l.Account)
            .ToListAsync();
        return  result;
    }

    public void CreatePost(Post post)
    {
        _dbContext.Posts.Add(post);
    }

    public void UpdatePost(Post post)
    {
        var postToUpdate =  ReadPostById(post.Id);
    
        postToUpdate.Title = post.Title;
        postToUpdate.Text = post.Text;
        
        _dbContext.Posts.Update(postToUpdate);
        
    }

    public void DeletePost(long id)
    {
        var removePost = ReadPostById(id);
        if (removePost == null)
        {
            throw new Exception($"Post with id {id} not found");
        }
        _dbContext.Posts.Remove(removePost);
    }

    public async Task<PostLike> LikePost(long postId, Account account)
    {
        var post = await ReadPostByIdAsync(postId);
        
        if (post == null)
        {
            throw new Exception($"Post with id {postId} not found");
        }
        
        var like = new Like{ LikeType = LikeType.ThumbsUp, Account = account};
        var postLike = new PostLike{ Like = like, Post = post};
       
        //post.PostLikes.Add(postLike);
        
        _dbContext.PostLikes.Add(postLike);
        
       // await _dbContext.SaveChangesAsync();
        
        return postLike;
    }

    public async Task<PostLike> DislikePost(long postId, Account account)
    {
        var post = await ReadPostByIdAsync(postId);
        var like = new Like{ LikeType = LikeType.ThumbsDown, Account = account};
        var postLike = new PostLike{ Like = like, Post = post};
        
        _dbContext.PostLikes.Add(postLike);

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

    public async Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account)
    {
        var post = await ReadPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        reaction.Account = account;
        var postReaction = new PostReaction{ Post = post, Reaction = reaction};
         _dbContext.PostReactions.Add(postReaction);
        return reaction;
    }
}