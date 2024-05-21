using Phygital.Domain.Feedback;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class FeedbackViewModel
{
    public IEnumerable<Post> Posts { get; set; }
    public ReactionDto Reaction { get; set; }
}