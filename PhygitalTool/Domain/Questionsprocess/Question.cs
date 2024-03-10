using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Datatypes;

namespace Phygital.Domain.Questionsprocess;

public class Question : FlowElement
{
    public long Id { get; set; }

    public Questiontype Questiontype { get; set; }

    [MaxLength(200)]
    public string Text { get; set; }
    public bool Active { get; set; }
    
    public int SequenceNumber { get; set; }
    
}