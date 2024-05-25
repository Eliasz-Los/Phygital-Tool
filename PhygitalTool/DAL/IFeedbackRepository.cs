using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithThemeByIdAsync(long id);
    Task<IEnumerable<Post>> ReadAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
    
    Task CreatePost(Post post);
    
    void UpdatePost(Post post);
    
    void DeletePost(long id);
    
    Task<PostLike> LikePost(long postId, Account account); 

    Task<PostLike> DislikePost(long postId, Account account);
    Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account);
    
    Task<PostLike> ReadDislikeByPostIdAndUserId(long postId, string currentAccountId);
    
    Task DeletePostDislikeByPostId(long postId, string id);

    Task<PostLike> ReadLikeByPostIdAndUserId(long postId, string currentAccountId);
    Task DeletePostLikeByPostId(long postId, string id);
    Task<int> ReadLikesCountByPostId(long postId);
    Task<int> ReadDislikesCountByPostId(long postId);
    Task<IEnumerable<PostReaction>> ReadReactionsOfPostByPostId(long postId);
}