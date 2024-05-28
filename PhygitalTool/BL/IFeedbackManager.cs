using Microsoft.AspNetCore.Http;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<Post> GetPostWithAccountAndWithThemeById(long id);
     Task<IEnumerable<Post>> GetAllPostsWithAccountWithThemeAndWithReactionsAndLikesOrderByDescPostTime();
     Task AddPost(string title, string text, long themeId, Account account, IFormFile imageFile);
     Task ChangePost(long postId, string title, string text, long themeId);
     void RemovePost(long postId);
     Task<PostLike> AddPostLikeByPostId(long postId, Account account);
     Task<PostLike> AddDislikePostByPostId(long postId, Account account);
     Task<Reaction> AddReactionToPostById(long postId, string content, Account account);
     Task<PostLike> GetDislikeByPostIdAndAccountId(long postId, string currentAccountId);
     Task RemovePostDislikeByPostIdAndAccountId(long postId, string id);
     Task<PostLike> GetLikeByPostIdAndAccountId(long postId, string currentAccountId);
     Task RemovePostLikeByPostIdAndAccountId(long postId, string id);
     Task<int> GetLikesCountByPostId(long postId);
     Task<int> GetDislikesCountByPostId(long postId);
     Task<IEnumerable<PostReaction>> GetReactionsWithAccountAndLikesOfPostByPostId(long postId);
     
     Task RemoveReactionToPostByPostIdAndReactionId(long postId , long reactionId);
     Task<Reaction> GetReactionWithAccountById(long reactionId);
     Task<ReactionLike> AddReactionLikeByReactionIdAndAccount(long reactionId, Account currentAccount);
     Task<ReactionLike> AddReactionDisLikeByReactionIdAndAccount(long reactionId, Account currentAccount);
     Task<int> GetLikesCountByReactionId(long reactionId);
     Task<int> GetDislikesCountByReactionId(long reactionId);
}