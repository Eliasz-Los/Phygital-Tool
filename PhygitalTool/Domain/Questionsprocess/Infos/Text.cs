using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Text : Info
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}