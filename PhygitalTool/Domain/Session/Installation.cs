namespace Phygital.Domain.Session;

public class Installation
{
    public int installationID { get; set; }
    public string location { get; set; }
    public ICollection<Participation> Participations { get; set; }

}