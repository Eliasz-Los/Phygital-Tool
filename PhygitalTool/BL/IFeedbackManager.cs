using Microsoft.AspNetCore.Http;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<Post> GetPostWithAccountAndWithThemeById(long id);
     Task<IEnumerable<Post>> GetAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
     Task AddPost(string title, string text, long themeId, Account account, IFormFile imageFile);
     Task ChangePost(long postId, string title, string text, long themeId);
     void RemovePost(long postId);
     Task<PostLike> AddPostLikeByPostId(long postId, Account account);
     Task<PostLike> AddDislikePostByPostId(long postId, Account account);
     Task<Reaction> AddReactionToPostById(long postId, string content, Account account);
     Task<PostLike> GetDislikeByPostIdAndUserId(long postId, string currentAccountId);
     Task RemovePostDislikeByPostId(long postId, string id);
     Task<PostLike> GetLikeByPostIdAndUserId(long postId, string currentAccountId);
     Task RemovePostLikeByPostId(long postId, string id);
     Task<int> GetLikesCountByPostId(long postId);
     Task<int> GetDislikesCountByPostId(long postId);
     Task<IEnumerable<PostReaction>> GetReactionsOfPostByPostId(long postId);
     
     Task RemoveReactionToPostById(long postId , long reactionId);
     Task<Reaction> GetReactionWithAccountById(long reactionId);
     Task<ReactionLike> AddReactionLikeByReactionId(long reactionId, Account currentAccount);
     Task<ReactionLike> AddReactionDisLikeByReactionId(long reactionId, Account currentAccount);
     Task<int> GetLikesCountByReactionId(long reactionId);
     Task<int> GetDislikesCountByReactionId(long reactionId);
     Task<ReactionLike> GetDislikeByReactionIdAndUserId(long reactionId, string currentAccountId);
     Task RemoveReactionDislikeByReactionIdAndUserId(long reactionId, string currentAccountId);
     Task<ReactionLike> GetLikeByReactionIdAndUserId(long reactionId, string currentAccountId);

     Task RemoveReactionLikeByReactionIdAndUserId(long reactionId, string currentAccountId);
}