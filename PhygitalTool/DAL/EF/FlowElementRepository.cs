using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.DAL.EF;

public class FlowElementRepository : IFlowElementRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FlowElementRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Question ReadQuestionById(long questionId)
    {
        // Try to find the question in OpenQuestions
        var openQuestion = _dbContext.OpenQuestions.Find(questionId);
        if (openQuestion != null)
        {
            return openQuestion;
        }

        // Try to find the question in SingleChoiceQuestions
        var singleChoiceQuestion = _dbContext.SingleChoiceQuestions.Find(questionId);
        if (singleChoiceQuestion != null)
        {
            return singleChoiceQuestion;
        }

        // Try to find the question in MultipleChoices
        var multipleChoiceQuestion = _dbContext.MultipleChoices.Find(questionId);
        if (multipleChoiceQuestion != null)
        {
            return multipleChoiceQuestion;
        }

        // Try to find the question in RangeQuestions
        var rangeQuestion = _dbContext.RangeQuestions.Find(questionId);
        if (rangeQuestion != null)
        {
            return rangeQuestion;
        }

        // If the question was not found in any of the tables, return null
        return null;
    }

    public IEnumerable<SingleChoiceQuestion> ReadSingleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _dbContext.SingleChoiceQuestions
            .Include(s => s.Options)
            .Where(scq => scq.Flow.Id == flowId)
            .ToList();
    }

    public IEnumerable<MultipleChoice> ReadMultipleChoiceQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _dbContext.MultipleChoices
            .Include(mc => mc.Options)
            .Where(mc => mc.Flow.Id == flowId)
            .ToList();
    }

    public IEnumerable<RangeQuestion> ReadRangeQuestionsWithOptionsOfFlowById(long flowId)
    {
        return _dbContext.RangeQuestions
            .Include(rq => rq.Options)
            .Where(rq => rq.Flow.Id == flowId)
            .ToList();
    }

    public IEnumerable<OpenQuestion> ReadOpenQuestionsWithAnswerOfFlowById(long flowId)
    {
        return _dbContext.OpenQuestions
            .Include(oq => oq.Answer)
            .Where(oq => oq.Flow.Id == flowId)
            .ToList();
    }

    public IEnumerable<Text> ReadTextInfosOfFlowById(long flowId)
    {
        var result = _dbContext.Texts.Where(t => t.Flow.Id == flowId).ToList();
        return result; //flow.FlowElements.OfType<Info>();
    }

    public IEnumerable<Image> ReadImageInfosOfFlowById(long flowId)
    {
        var result = _dbContext.Images.Where(i => i.Flow.Id == flowId).ToList();
        return result;
    }

    public IEnumerable<Video> ReadVideoInfosOfFlowById(long flowId)
    {
        var result = _dbContext.Videos.Where(v => v.Flow.Id == flowId).ToList();
        return result;
    }

    public Option ReadOptionByText(string optionText)
    {
        return _dbContext.Options.FirstOrDefault(o => o.OptionText.ToLower().Equals(optionText.ToLower()));
    }
}