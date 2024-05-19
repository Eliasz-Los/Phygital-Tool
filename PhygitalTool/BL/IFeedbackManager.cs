using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<Post> GetPostWithThemeByIdAsync(long id);
     Task<IEnumerable<Post>> GetAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
     
     void AddPost(string title, string text, long themeId, Account account);
     
     Task ChangePost(long postId, string title, string text, long themeId);
     
     void RemovePost(long postId);
     
     Task<PostLike> AddPostLikeByPostId(long postId, Account account);
     
     Task<PostLike> AddDislikePostByPostId(long postId, Account account);
     Task<Reaction> AddReactionToPostById(long postId, string content, Account account);
     Task<PostLike> GetDislikeByPostIdAndUserId(long postId, string currentAccountId);
     Task RemovePostDislikeByPostId(long postId, string id);
     Task<PostLike> GetLikeByPostIdAndUserId(long postId, string currentAccountId);
     Task RemovePostLikeByPostId(long postId, string id);
}