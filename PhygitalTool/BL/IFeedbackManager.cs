using Phygital.Domain.Feedback;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes();
     
     void AddPost(string title, string text);
     
     Task<PostLike> AddPostLikeByPostId(long postId);
     
     Task<PostLike> RemovePostLikeByPostId(long postId, long likeId);
     
     Task<Reaction> AddReactionToPostById(long postId, string content);
}