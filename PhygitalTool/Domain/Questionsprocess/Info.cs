using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public abstract class Info : FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Thema SubThema { get; set; }
    public string Title { get; set; }
}