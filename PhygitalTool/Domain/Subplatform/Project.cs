using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Subplatform;

public class Project
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<Flow> Flows { get; set; }
    public ICollection<Version> Versions { get; set; }
}