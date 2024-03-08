using Domain.Themas;
using Phygital.Domain.Questionsprocess;

namespace Domain;

public class FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Thema Thema { get; set; }
}