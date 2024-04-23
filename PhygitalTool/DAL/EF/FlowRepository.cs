using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;

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
        _dbContext.SaveChanges();
    }

    public void DeleteFlow(long id)
    {
        var flowsToDelete = _dbContext.Flows.Find(id);
        _dbContext.Flows.Remove(flowsToDelete);
        _dbContext.SaveChanges();
    }
}