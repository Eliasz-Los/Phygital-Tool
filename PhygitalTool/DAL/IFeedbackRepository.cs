using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL;

//TODO: async toevoegen
public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithThemeByIdAsync(long id);
    Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes();
    
    void CreatePost(Post post);
    
    void UpdatePost(Post post);
    
    void DeletePost(long id);
    
    Task<PostLike> LikePost(long postId, Account account); 

    Task<PostLike> DislikePost(long postId, Account account);
    Task<PostLike> DeletePostLike(long postId, long likeId);
    
    Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account);
    
    
    
}