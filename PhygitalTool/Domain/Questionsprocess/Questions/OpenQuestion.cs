using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class OpenQuestion : Question
{
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public override string Text { get; set; }
    public bool Active { get; set; }
    public override int SequenceNumber { get; set; }
    public Answer Answer { get; set; }
}