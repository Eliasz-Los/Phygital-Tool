using Phygital.Domain.Datatypes;

namespace Phygital.Domain.Session;

public class Session
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SessionType SessionType { get; set; }
    
    public Installation Installation { get; set; }
    public Participation Participation { get; set; }
    
}