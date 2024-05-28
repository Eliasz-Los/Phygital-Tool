using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostByIdAsync(long id);
    Task<Post> ReadPostWithAccountAndWithThemeById(long id);
    Task<IEnumerable<Post>> ReadAllPostsWithAccountWithThemeAndWithReactionsAndLikesOrderByDescPostTime();
    
    Task CreatePost(Post post);
    
    void UpdatePost(Post post);
    
    void DeletePost(long id);
    
    Task<PostLike> LikePost(long postId, Account account); 

    Task<PostLike> DislikePost(long postId, Account account);
    Task<Reaction> CreateReactionToPostById(long postId, Reaction reaction, Account account);
    
    Task<PostLike> ReadDislikeByPostIdAndAccountId(long postId, string currentAccountId);
    
    Task DeletePostDislikeByPostIdAndAccountId(long postId, string currenAccountId);

    Task<PostLike> ReadLikeByPostIdAndAccountId(long postId, string currentAccountId);
    Task DeletePostLikeByPostIdAndAccountId(long postId, string currentAccountId);
    Task<int> ReadLikesCountByPostId(long postId);
    Task<int> ReadDislikesCountByPostId(long postId);
    Task<IEnumerable<PostReaction>> ReadReactionsWithAccountAndLikesOfPostByPostId(long postId);
    
    Task DeleteReactionToPostByPostIdAndReactionId(long postId, long reactionId);
    Task<Reaction> ReadReactionWithAccountById(long reactionId);
    Task<ReactionLike> CreateReactionLikeByReactionIdAndAccount(long reactionId, Account currentAccount);
    Task<ReactionLike> CreateReactionDisLikeByReactionIdAndAccount(long reactionId, Account currentAccount);
    Task<int> ReadLikesCountByReactionId(long reactionId);
    Task<int> ReadDislikesCountByReactionId(long reactionId);
}