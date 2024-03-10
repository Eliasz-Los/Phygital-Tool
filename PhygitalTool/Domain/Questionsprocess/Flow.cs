using Domain;
using Domain.Datatypes;
using Domain.Session;
using Domain.Themas;

namespace Phygital.Domain.Questionsprocess;

// The flow class can be filled up with flowElements and can be of a certain type (Linear or Cirular)
public class Flow
{ // TODO hoodthema toevoegen?
    public long Id { get; set; }

    // A flow belongs to a specific type (Linear or Circular)
    public Flowtype Flowtype { get; set; }
    
    // A flow can either be active or inactive
    public bool IsOpen { get; set; }
    
    // A flow can consist of many Themes
    public Thema Thema { get; set; }
    
    
    // A list of elements used in the flow
    public ICollection<FlowElement> FlowElements { get; set; }
    public ICollection<Participation> Participations { get; set; }
    
    // A Collection of the participations the flow has
    public ICollection<Participation> Participations { get; set; }


    // This default constructor sets the flow mode to linear and closes the flow so the administrator can edit it later
    public Flow()
    {
        Flowtype = Flowtype.linear;
        IsOpen = false;
    }

    // This constructor is used for flows that don't have participations (yet).
    public Flow(Flowtype flowtype, bool isOpen, ICollection<Thema> themas, ICollection<FlowElement> flowElements)
    {
        Flowtype = flowtype;
        IsOpen = isOpen;
        Themas = themas;
        FlowElements = flowElements;
    }

    // This constructor is used for flows that do have participation
    public Flow(Flowtype flowtype, bool isOpen, ICollection<Thema> themas, ICollection<FlowElement> flowElements, ICollection<Participation> participations)
    {
        Flowtype = flowtype;
        IsOpen = isOpen;
        Themas = themas;
        FlowElements = flowElements;
        Participations = participations;
    }
}