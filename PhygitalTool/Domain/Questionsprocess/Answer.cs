using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

// The Answer class represents the answer a user has given to a specific question
public class Answer : FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }

    [MaxLength(255)]
    public string Text { get; set; }
    
    // Link to the question
    public Question Question { get; set; }
}