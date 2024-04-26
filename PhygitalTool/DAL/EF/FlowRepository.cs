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

    public void UpdateFlow(Flow flow)
    {
        _dbContext.Flows.Update(flow);
    }

    public void DeleteFlow(long id)
    {
        var flowToDelete = _dbContext.Flows.Find(id);
        _dbContext.Flows.Remove(flowToDelete);
    }
}