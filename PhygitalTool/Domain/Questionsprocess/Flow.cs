using Domain;
using Domain.Datatypes;
using Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Flow
{
    public long Id { get; set; }

    public Flowtype FlowType { get; set; }
    
    public bool IsOpen { get; set; }
    
    public ICollection<Thema> Themas { get; set; }
    
    public ICollection<FlowElement> FlowElements { get; set; }
    

}