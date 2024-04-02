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

    public Flow ReadFlowById(long id)
    {
        return _dbContext.Flows.Find(id);
    }

    public void CreateAnswer(Answer answer)
    {
        _dbContext.Answers.Add(answer);
        _dbContext.SaveChanges();
    }


    // scq = SingleChoiceQuestion
    public IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        IQueryable<SingleChoiceQuestion> scq = _dbContext.SingleChoiceQuestions;
        var result = scq.Include(s => s.Options)
            .Where(scq => scq.Flow.Id == flowId);
        return result;
    }

    public IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        IQueryable<MultipleChoice> mcq = _dbContext.MultipleChoices;
        var result = mcq.Include(m => m.Options)
            .Where(mcq => mcq.Flow.Id == flowId);
        return result;
    }

    public IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId)
    {
        IQueryable<RangeQuestion> rq = _dbContext.RangeQuestions;
        var result = rq.Include(r => r.Options)
            .Where(rq => rq.Flow.Id == flowId);
        return result;
    }

    public IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId)
    {
        IQueryable<OpenQuestion> oq = _dbContext.OpenQuestions;
        var result = oq.Include(o => o.Answer)
            .Where(oq => oq.Flow.Id == flowId);
        return result;
    }

    public IEnumerable<Theme> ReadSubThemasFlow(long flowId)
    {
        return _dbContext.Themas.Where(t => t.Flows.Any(f => f.Id == flowId));
    }
}