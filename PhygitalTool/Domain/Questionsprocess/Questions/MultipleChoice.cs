using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class MultipleChoice : Question
{
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public override string Text { get; set; }

    public override bool Active { get; set; }
    public override int SequenceNumber { get; set; }
    public ICollection<Option> Options { get; set; }
}