namespace Phygital.Domain.Session;

public class Note
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public Participation Participation { get; set; }
}