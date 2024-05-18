using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithThemeByIdAsync(long id);
    Task<IEnumerable<Post>> ReadAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
    
    void CreatePost(Post post);
    
    void UpdatePost(Post post);
    
    void DeletePost(long id);
    
    Task<PostLike> LikePost(long postId, Account account); 

    Task<PostLike> DislikePost(long postId, Account account);
    Task<PostLike> DeletePostLike(long postId, long likeId);
    
    Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account);
    
    Task<PostLike> ReadDislikeByPostIdAndUserId(long postId, string currentAccountId);
    
    Task DeletePostDislikeByPostId(long postId, string id);

    Task<PostLike> ReadLikeByPostIdAndUserId(long postId, string currentAccountId);
    Task DeletePostLikeByPostId(long postId, string id);
}