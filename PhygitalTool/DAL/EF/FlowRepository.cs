using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
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
        return _dbContext.Flows;
    }
}