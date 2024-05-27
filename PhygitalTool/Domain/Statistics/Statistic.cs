namespace Phygital.Domain.Statistics;

public class Statistic
{
    public string Title { get; set; }
    public Dictionary<string, int> Answers { get; set; }
    public Dictionary<string, int> Participations { get; set; }
    
    public Statistic()
    {
        Answers = new Dictionary<string, int>();
        Participations = new Dictionary<string, int>();
    }
}