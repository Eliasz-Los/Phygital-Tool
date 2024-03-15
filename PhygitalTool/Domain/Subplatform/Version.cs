namespace Phygital.Domain.Subplatform;


//Een versie die meerdere projecten heeft maakt niet veel sense als ik nu over nadenk?
//Een versie is hier meer van een note van wat er ge edit was maar misschien moeten we dit veranderen.
public class Version
{
    public DateTime DateTimeOfEdit { get; set; }
    public string Description { get; set; }
    public ICollection<Project> Projects { get; set; }
}