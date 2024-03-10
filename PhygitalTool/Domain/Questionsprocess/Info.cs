using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public interface Info: FlowElement
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Thema SubThema { get; set; }
    public string Title { get; set; }
}