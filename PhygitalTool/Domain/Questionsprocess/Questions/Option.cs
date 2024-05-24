using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Questionsprocess.Questions;

public class Option 
{
    public long Id { get; set; }
    public MultipleChoice MultipleChoice { get; set; }
    public RangeQuestion RangeQuestion { get; set; }
    public SingleChoiceQuestion SingleChoiceQuestion { get; set; }
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string OptionText { get; set; }
    public Answer Answer { get; set; }
}