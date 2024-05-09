using Phygital.Domain.Feedback;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithThemeByIdAsync(long id);
    Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes();
    
    void CreatePost(Post post);
    
    void UpdatePost(Post post);
    
    void DeletePost(long id);
    
    Task<PostLike> LikePost(long postId); //long userId misschien later wanneer we die klasse eens fixen

    Task<PostLike> DislikePost(long postId);
    Task<PostLike> DeletePostLike(long postId, long likeId);
    
    Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction);
    
    
    
}