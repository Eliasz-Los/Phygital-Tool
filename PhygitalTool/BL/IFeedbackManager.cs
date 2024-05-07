using Phygital.Domain.Feedback;

namespace Phygital.BL;

public interface IFeedbackManager
{
     Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes();
}