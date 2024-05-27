using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class SingleChoiceQuestion : Question
{
    // public Flow Flow { get; set; }
    // public Theme SubTheme { get; set; }
    [MaxLength(500, ErrorMessage = "Text is too long, max 500 characters.")]
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    
    public ICollection<Option> Options { get; set; }
}