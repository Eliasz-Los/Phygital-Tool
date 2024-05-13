namespace Phygital.Domain.Session;

public class Installation
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int PostalCode { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public ICollection<Session> Sessions { get; set; }
}