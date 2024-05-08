using Phygital.Domain.Feedback;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

//Puur zodat reaction gecheckt kan worden dat het geen leeg zijn en kan valideren
//TODO: Checken of dit wel nodig (niet echt nodig maar andes als je lege wilt invoegen geeft het een error), docent vragen.
public class FeedbackViewModel
{
    public IEnumerable<Post> Posts { get; set; }
    public ReactionDto Reaction { get; set; }
}