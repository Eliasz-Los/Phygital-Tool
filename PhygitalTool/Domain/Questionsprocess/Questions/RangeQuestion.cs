using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Questions;

public class RangeQuestion : Question
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }

    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    
    public ICollection<Option> Options { get; set; }
}