using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess.Infos;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Themas;

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

    public IEnumerable<OpenQuestion> ReadAllOpenQuestionsByTheme(Theme subTheme)
    {
        // Todo aan het debuggen hier
        var th1 = new Theme { Title = "Politiek", Description = "Simpele vragen rond politiek" };
        var test = _dbContext.OpenQuestions.Include(q => q.SubTheme).Where(q => q.SubTheme.Title.Equals(th1.Title))
            .ToList();
        long id = 3;
        var openQuestion = _dbContext.OpenQuestions.Include(q => q.SubTheme).FirstOrDefault(q => q.Id == id);
        return test;
    }

    public IEnumerable<SingleChoiceQuestion> ReadAllSingleChoiceQuestionsByTheme(Theme subTheme)
    {
        return _dbContext.SingleChoiceQuestions.Where(q => q.SubTheme == subTheme);
    }

    public IEnumerable<RangeQuestion> ReadAllRangeQuestionsByTheme(Theme subTheme)
    {
        return _dbContext.RangeQuestions.Where(q => q.SubTheme == subTheme).ToList();
    }

    public OpenQuestion ReadOpenQuestionById(long id)
    {
        return _dbContext.OpenQuestions.Find(id);
    }

    public SingleChoiceQuestion ReadSingleQuestionById(long id)
    {
        return _dbContext.SingleChoiceQuestions.Find(id);
    }

    public MultipleChoice ReadMultipleChoiceQuestionById(long id)
    {
        return _dbContext.MultipleChoices.Find(id);
    }

    public RangeQuestion ReadRangeQuestionById(long id)
    {
        return _dbContext.RangeQuestions.Find(id);
    }

    public void CreateOpenQuestion(OpenQuestion openQuestion)
    {
        _dbContext.OpenQuestions.Add(openQuestion);
    }

    public void CreateOption(Option option)
    {
        _dbContext.Options.Add(option);
    }

    public void CreateMultipleChoiceQuestion(MultipleChoice multipleChoiceQuestion)
    {
        _dbContext.MultipleChoices.Add(multipleChoiceQuestion);
    }

    public void CreateSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion)
    {
        _dbContext.SingleChoiceQuestions.Add(singleChoiceQuestion);
    }

    public void CreateRangeQuestion(RangeQuestion rangeQuestion)
    {
        _dbContext.RangeQuestions.Add(rangeQuestion);
    }

    public void CreateImage(Image image)
    {
        _dbContext.Images.Add(image);
    }

    public void CreateText(Text text)
    {
        _dbContext.Texts.Add(text);
    }

    public void CreateVideo(Video video)
    {
        _dbContext.Videos.Add(video);
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

    //todo: debug this method
    public IEnumerable<OpenQuestion> ReadAllOpenQuestionByThemeId(long themeId)
    {
        var test = _dbContext.OpenQuestions.Include(q => q.SubTheme);
        // var result = _dbContext.OpenQuestions.Where(o => o.SubTheme.Id == themeId);
        return test;
    }

    public IEnumerable<OpenQuestion> ReadAllOpenQuestionByFlowId(long flowId)
    {
        return _dbContext.OpenQuestions.Where(q => q.Flow.Id == flowId).Include(q => q.SubTheme);
    }

    public IEnumerable<MultipleChoice> ReadAllMultipleChoiceQuestionByFlowId(long flowId)
    {
        return _dbContext.MultipleChoices.Where(q => q.Flow.Id == flowId);
    }

    public IEnumerable<SingleChoiceQuestion> ReadAllSingleQuestionByFlowId(long flowId)
    {
        return _dbContext.SingleChoiceQuestions.Where(q => q.Flow.Id == flowId);
    }

    public IEnumerable<RangeQuestion> ReadAllRangeQuestionByFlowId(long flowId)
    {
        return _dbContext.RangeQuestions.Where(q => q.Flow.Id == flowId);
    }

    // todo Testing met openquestion
    public void UpdateActive(long questionId)
    {
        var question = _dbContext.OpenQuestions.SingleOrDefault(q => q.Id == questionId);

        if (question != null)
        {
            question.Active = !question.Active;
            _dbContext.SaveChanges();
        }
        else
        {
            throw new Exception("Question not found.");
        }
    }


    public void DeleteOpenQuestionFromFlow(long questionId)
    {
        var flowElement = _dbContext.FlowElements.Find(questionId);
           
        if( flowElement!= null)
        {
            flowElement.Flow = null;
            //flowElement.Active = false;
            /*flowElement.Flow = null;
            flowElement.SubTheme = null;
            flowElement.Questions = null;
            flowElement.Infos = null;*/
            //_dbContext.FlowElements.Remove(flowElement);
        }
    }

    /*public IEnumerable<OpenQuestion> ReadAllOpenQuestionsByTheme(Theme subTheme)
    {
        // Todo aan het debuggen hier
        /*var th1 = new Theme { Title = "Politiek", Description = "Simpele vragen rond politiek" };
        var test = _dbContext.OpenQuestions.Include(q => q.SubTheme).Where(q => q.SubTheme.Title.Equals(th1.Title)).ToList();
        long id = 3;
        var openQuestion = _dbContext.OpenQuestions.Include(q => q.SubTheme).FirstOrDefault(q => q.Id == id);
        return test;#1#
        var result = _dbContext.OpenQuestions.Select(o => o);
        return result;

    }*/
}