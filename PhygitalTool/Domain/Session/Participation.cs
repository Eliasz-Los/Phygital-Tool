using Phygital.Domain.Questionsprocess;

namespace Domain.Session;

public class Participation
{
    public int participationID { get; set; }
    public TimeOnly participationDuration { get; set; }
    public int amountOfParticipations { get; set; }
    public Installation Installation { get; set; }

    public Flow flow { get; set; }
    public ICollection<Note> Notes { get; set; }
}