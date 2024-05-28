using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Questionsprocess.Questions;

public class SingleChoiceQuestion : Question
{
    [MaxLength(500, ErrorMessage = "Text is too long, max 500 characters.")]
    public override string Text { get; set; }
    public override bool Active { get; set; }
    public override int SequenceNumber { get; set; }
    
    public ICollection<Option> Options { get; set; }
}