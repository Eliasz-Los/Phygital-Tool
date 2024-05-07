using Phygital.DAL;
using Phygital.Domain.Feedback;

namespace Phygital.BL.Managers;

public class FeedbackManager : IFeedbackManager
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackManager(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes()
    {
        return await _feedbackRepository.ReadAllPostsWithReactionsAndLikes();
    }
}