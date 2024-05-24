using Phygital.Domain.Datatypes;
using Phygital.Domain.Session;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Flow
{
    public long Id { get; set; }
    public Flowtype FlowType { get; set; }
    public bool IsOpen { get; set; }
    public Theme Theme { get; set; }
    public ICollection<FlowElement> FlowElements { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public ICollection<Participation> Participations { get; set; }
}