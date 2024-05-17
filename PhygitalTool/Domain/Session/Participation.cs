using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Session;

public class Participation
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int AmountOfParticipants { get; set; }
    public Session Session { get; set; }
    public Flow Flow { get; set; }
    public ICollection<Note> Notes { get; set; }
}