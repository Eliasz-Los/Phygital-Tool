namespace Phygital.Domain.Subplatform;

public class Version
{
    public long Id { get; set; }
    public DateTime DateTimeOfEdit { get; set; }
    public string Description { get; set; }
    public Project Project { get; set; }
}