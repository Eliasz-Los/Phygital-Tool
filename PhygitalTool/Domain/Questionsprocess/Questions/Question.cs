using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public abstract class Question : FlowElement
{
    // public Flow Flow { get; set; }
    public Theme Theme { get; set; }
}