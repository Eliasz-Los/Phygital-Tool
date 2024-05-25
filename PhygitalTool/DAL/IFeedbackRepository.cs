using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithAccountAndWithThemeById(long id);
    Task<IEnumerable<Post>> ReadAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
    
    void CreatePost(Post post);
    
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
    
    Task DeleteReactionToPostById(long postId, long reactionId);
    Task<Reaction> ReadReactionWithAccountById(long reactionId);
    Task<ReactionLike> CreateReactionLikeByReactionId(long reactionId, Account currentAccount);
    Task<ReactionLike> CreateReactionDisLikeByReactionId(long reactionId, Account currentAccount);
    Task<int> ReadLikesCountByReactionId(long reactionId);
    Task<int> ReadDislikesCountByReactionId(long reactionId);
    Task<ReactionLike> ReadDislikeByReactionIdAndUserId(long reactionId, string currentAccountId);
    Task DeleteReactionDislikeByReactionIdAndUserId(long reactionId, string currentAccountId);
    Task<ReactionLike> ReadLikeByReactionIdAndUserId(long reactionId, string currentAccountId);
    Task DeleteReactionLikeByReactionIdAndUserId(long reactionId, string currentAccountId);
}