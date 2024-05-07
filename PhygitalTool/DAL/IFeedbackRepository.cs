using Phygital.Domain.Feedback;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    
    Task<Post> ReadPostById(long id);
    Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes();
    
    void CreatePost(Post post);
    
    Task<PostLike> LikePost(long postId); //long userId misschien later wanneer we die klasse eens fixen
    Task<PostLike> DeletePostLike(long postId, long likeId);
    
    
}