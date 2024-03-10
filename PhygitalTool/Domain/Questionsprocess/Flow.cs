using Domain;
using Domain.Datatypes;
using Domain.Session;
using Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Flow
{
    public long Id { get; set; }

    public Flowtype Flowtype { get; set; }
    
    public bool IsOpen { get; set; }
    
    public Thema Thema { get; set; }
    
    public ICollection<FlowElement> FlowElements { get; set; }
    public ICollection<Participation> Participations { get; set; }
}