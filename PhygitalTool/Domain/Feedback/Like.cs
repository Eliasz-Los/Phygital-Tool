using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Like
// TODO moet like een klasse zijn? zo ja hoe weet deze of het een like op een post of een reactie is
// We moeten statitieken kunnen bijhouden en zien welke Like bij welke User toebehoort
// en Reaction of Post waardoor dit een klasse moet zijn
// Navigatie door klasse Post en Reaction toe te voegen
{
    public long Id { get; set; }
    public Post Post { get; set; }
    public Reaction Reaction { get; set; }
    
    public Account Account { get; set; }

}