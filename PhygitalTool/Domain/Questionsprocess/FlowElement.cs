using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

// A flowelement 
public abstract class FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    
    public ICollection<Info> Infos { get; set; }
    public ICollection<Question> Questions { get; set; }
}