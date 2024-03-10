namespace Domain.Deelplatform;

public class Version
{
    public DateTime dateTimeOfEdit { get; set; }

    public string Description { get; set; }
    public ICollection<Project> Projects { get; set; }
}