using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Session;


//Sessie gaat nog wat verandert moeten worden omdat guest zelf geen entiteit is maar een rol van account
public class Participation
{
    public long Id { get; set; }
    public TimeOnly Duration { get; set; }
    public int AmountOfParticipants { get; set; }
    public Installation Installation { get; set; }

    public Flow flow { get; set; }
    public ICollection<Note> Notes { get; set; }
}