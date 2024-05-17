using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Session;

public class Participation
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration
    {
        get
        {
            var duration = EndTime - StartTime;
            return $"{duration.Hours}h {duration.Minutes}m {Math.Ceiling((double)duration.Seconds)}s";
        }
    }
    public int AmountOfParticipants { get; set; }
    public Session Session { get; set; }
    public Flow Flow { get; set; }
    public ICollection<Note> Notes { get; set; }
}