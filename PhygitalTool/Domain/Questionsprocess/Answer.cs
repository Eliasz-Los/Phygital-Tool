using System.ComponentModel.DataAnnotations;

namespace Domain.Questionsprocess;

public class Answer : FlowElement
{
    public long Id { get; set; }
    [MaxLength(255)]
    public string Text { get; set; }
}