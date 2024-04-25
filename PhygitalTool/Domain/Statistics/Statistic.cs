namespace Phygital.Domain.Statistics;

public class Statistic
{
    public string QuestionText { get; set; }
    public Dictionary<string, int> Answers { get; set; }
    
    public Statistic()
    {
        Answers = new Dictionary<string, int>();
    }
}