using Phygital.DAL;

namespace Phygital.BL.Managers;

public class FeedbackManager : IFeedbackManager
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackManager(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }
}