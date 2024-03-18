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

    // scq = SingleChoiceQuestion
    public IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsOfFlow(long flowId)
    {
        return _dbContext.SingleChoiceQuestions.Where(scq => scq.Flow.Id == flowId);
    }

    public IEnumerable<Theme> ReadSubThemasFlow(long flowId)
    {
        return _dbContext.Themas.Where(t => t.Flows.Any(f => f.Id == flowId));
    }
}