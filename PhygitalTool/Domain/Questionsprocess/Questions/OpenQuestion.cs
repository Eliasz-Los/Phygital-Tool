using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class OpenQuestion : Question
{
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    public Answer Answer { get; set; }
}