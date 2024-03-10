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

    public Flowtype FlowType { get; set; }


    
    // A flow can either be active or inactive
    public bool IsOpen { get; set; }
    
    // A flow can consist of one main thema
    public Thema Thema { get; set; }
    
    
    // A list of elements used in the flow
    public ICollection<FlowElement> FlowElements { get; set; }
    
    // A Collection of the participations the flow has
    public ICollection<Participation> Participations { get; set; }
    
}