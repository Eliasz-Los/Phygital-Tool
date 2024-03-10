namespace Phygital.Domain.Session;

public class Note
{
    public int installationID { get; set; }
    public string comment { get; set; }
    public Participation Participation { get; set; }
}