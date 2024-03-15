namespace Phygital.Domain.Session;

public class Installation
{
    public long Id { get; set; }
    public string Location { get; set; }
    public ICollection<Session> Sessions { get; set; }
}