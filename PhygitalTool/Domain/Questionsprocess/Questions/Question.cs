using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public abstract class Question : FlowElement
{
    public abstract string Text { get; set; }
    
    public abstract int SequenceNumber { get; set; }
    
    // public Flow Flow { get; set; }
    public Theme Theme { get; set; }
}