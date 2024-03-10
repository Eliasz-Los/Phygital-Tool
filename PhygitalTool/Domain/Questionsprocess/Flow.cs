using Domain;
using Domain.Datatypes;
using Domain.Session;
using Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

public class Flow
{ // TODO hoodthema toevoegen?
    public long Id { get; set; }

    public Flowtype Flowtype { get; set; }
    
    public bool IsOpen { get; set; }
    
    public ICollection<Thema> Themas { get; set; }
    
    public ICollection<FlowElement> FlowElements { get; set; }
    
    public ICollection<Participation> Participations { get; set; }


    public Flow(Flowtype flowtype, bool isOpen, ICollection<Thema> themas, ICollection<FlowElement> flowElements)
    {
        Flowtype = flowtype;
        IsOpen = isOpen;
        Themas = themas;
        FlowElements = flowElements;
    }
}