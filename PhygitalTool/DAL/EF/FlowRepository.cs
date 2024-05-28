using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;

namespace Phygital.DAL.EF;

public class FlowRepository : IFlowRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FlowRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Flow> ReadAllFlows()
    {
        return _dbContext.Flows.Include(f => f.Theme);
    }

    public Flow ReadFlowById(long id)
    {
        return _dbContext.Flows.Find(id);
    }

    public Flow ReadFlowAndThemeById(long id)
    {
        return _dbContext.Flows.Include(f => f.Theme).FirstOrDefault(f => f.Id == id);
    }

    public void UpdateFlow(Flow flow)
    {
        _dbContext.Flows.Update(flow);
    }

    public void DeleteFlow(long id)
    {
        var flowToDelete = _dbContext.Flows.Find(id);
        
        // Omdat we de Flow willen verwijderen, moeten we eerst alle FlowElements die aan deze Flow gerelateerd zijn hun link verbreken oftwel CascadeDelete().
        var relatedFlowElements = _dbContext.FlowElements.Where(fe => fe.Flow.Id == id);
        foreach (var flowElement in relatedFlowElements)
        {
            flowElement.Flow = null; 
        }
        
        var relatedAnswers = _dbContext.Answers.Where(a => a.Flow.Id == id);
        foreach (var answer in relatedAnswers)
        {
            answer.Flow = null;
        }
        
        var relatedParticipations = _dbContext.Participations.Where(p => p.Flow.Id == id);
        foreach (var participation in relatedParticipations)
        {
            participation.Flow = null;
        }
        
        _dbContext.Flows.Remove(flowToDelete!);
    }

    public void CreateFlow(Flow flow)
    {
        _dbContext.Flows.Add(flow);
    }
}