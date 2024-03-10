using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Thema SubThema { get; set; }
}