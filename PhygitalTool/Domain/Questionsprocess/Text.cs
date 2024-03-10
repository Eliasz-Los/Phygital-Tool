using Domain.Questionsprocess;

namespace Phygital.Domain.Questionsprocess;

public class Text : Info
{
    public long Id { get; set; }

    public string Content { get; set; }

    public Text(string content)
    {
        Content = content;
    }
}