using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Question : FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    public Questiontype Questiontype { get; set; }

    [MaxLength(200)]
    public string Text { get; set; }
    public bool Active { get; set; }
    public int SequenceNumber { get; set; }
    
    public Answer Answer { get; set; }
}