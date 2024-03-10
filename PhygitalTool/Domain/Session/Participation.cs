using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Session;

public class Participation
{
    public long Id { get; set; }
    public TimeOnly participationDuration { get; set; }
    public int amountOfParticipations { get; set; }
    public Installation Installation { get; set; }

    public Flow flow { get; set; }
    public ICollection<Note> Notes { get; set; }
}