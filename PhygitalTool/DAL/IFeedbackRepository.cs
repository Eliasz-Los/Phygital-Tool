using Phygital.Domain.Feedback;

namespace Phygital.DAL;

public interface IFeedbackRepository
{
    Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes();
}