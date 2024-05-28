using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class MultipleChoice : Question
{
    [MaxLength(500, ErrorMessage = "Text is too long, max 500 characters.")]
    public override string Text { get; set; }
    public bool Active { get; set; }
    public override int SequenceNumber { get; set; }
    public ICollection<Option> Options { get; set; }
}