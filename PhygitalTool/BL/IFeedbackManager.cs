using Phygital.Domain.Feedback;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<Post> GetPostByIdAsync(long postId);

     Task<Post> GetPostWithThemeByIdAsync(long id);
     Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes();
     
     void AddPost(string title, string text, long themeId);
     
     //Hier wel een async omdat we een await gebruiken in de controller en we w8 tot de repository klaar is
     Task ChangePost(long postId, string title, string text, long themeId);
     
     void RemovePost(long postId);
     
     Task<PostLike> AddPostLikeByPostId(long postId);
     
     Task<PostLike> AddDislikePostByPostId(long postId);
     
     Task<PostLike> RemovePostLikeByPostId(long postId, long likeId);
     
     Task<Reaction> AddReactionToPostById(long postId, string content);
}