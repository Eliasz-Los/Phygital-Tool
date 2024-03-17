using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Image : Info
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    public string Title { get; set; }

    // TODO image zelf met een url?
    
    public string AltText { get; set; }
}