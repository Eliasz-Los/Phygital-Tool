using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Statistics;

namespace Phygital.DAL.EF;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly IFlowRepository _flowRepository;
    private readonly PhygitalDbContext _dbContext;

    public StatisticsRepository(IFlowRepository flowRepository, PhygitalDbContext dbContext)
    {
        _flowRepository = flowRepository;
        _dbContext = dbContext;
    }


    public IEnumerable<Statistic> GetFlowStatistics(long flowId)
    {
        var flow = _flowRepository.ReadFlowById(flowId);
        
        var openQuestions = _flowRepository.ReadOpenQuestionsWithAnswerOfFlowById(flowId);
        var singleChoiceQuestions = _flowRepository.ReadSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
        var multipleChoiceQuestions = _dbContext.MultipleChoices
            .Include(mc => mc.Options)
            .ThenInclude(o => o.Answer)
            .Where(mc => mc.Flow.Id == flowId)
            .ToList();
        var rangeQuestions = _flowRepository.ReadRangeQuestionsWithOptionsOfFlowById(flowId);
        
        var answers = _dbContext.Answers
            .Include(a => a.ChosenOptions)
            .Where(a => a.Flow != null && a.Flow.Id == flowId && a.MultipleChoice != null || a.SingleChoiceQuestion != null || a.RangeQuestion != null || a.OpenQuestion != null)
            .ToList();

        var stats = new List<Statistic>();
        
        //group answers by question
        var groupedAnswers = answers.GroupBy(a => a.MultipleChoice?.Text ?? a.SingleChoiceQuestion?.Text ?? a.RangeQuestion?.Text ?? a.OpenQuestion?.Text);


        foreach (var group in groupedAnswers)
        {
            var stat = new Statistic();
            stat.QuestionText = group.Key;

            foreach (var answer in group)
            {
                foreach (var option in answer.ChosenOptions)
                {
                    if (stat.Answers.ContainsKey(option.OptionText))
                    {
                        stat.Answers[option.OptionText]++;
                    }
                    else
                    {
                        stat.Answers[option.OptionText] = 1;
                    }
                }
            }

            stats.Add(stat);
        }


        /*foreach (var question in multipleChoiceQuestions)
        {
            var statistic = new Statistic();
            statistic.QuestionText = question.Text;
            foreach (var option in question.Options)
            {
               //var answerCount = answers.Count(a => a != null && option.Answer != null && a.Id == option.Answer.Id);
               //var answerCount = answers.Count(a => a.MultipleChoice!= null && a.MultipleChoice.Id == option.MultipleChoice.Id);
               var answerCount = answers.Count(a => a.ChosenOptions.Contains(option)); // && a.Id == option.Answer.Id
                statistic.Answers.Add(option.OptionText, answerCount);
            }
            stats.Add(statistic);
        }

        foreach (var question in singleChoiceQuestions)
        {
            var statistic = new Statistic();
            statistic.QuestionText = question.Text;
            foreach (var option in question.Options)
            {
                var answerCount = answers.Count(a => a.SingleChoiceQuestion!= null && a.SingleChoiceQuestion.Id == option.SingleChoiceQuestion.Id);

                //var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                statistic.Answers.Add(option.OptionText, answerCount);
            }
            stats.Add(statistic);
        }

        foreach (var question in rangeQuestions)
        {
            var statistic = new Statistic();
            statistic.QuestionText = question.Text;
            foreach (var option in question.Options)
            {
                //var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                var answerCount = answers.Count(a => a.RangeQuestion!= null && a.RangeQuestion.Id == option.RangeQuestion.Id);
                statistic.Answers.Add(option.OptionText, answerCount);
            }
            stats.Add(statistic);
        }*/
        
        /*TODO: GEEN ID in openquestion, object not set to en instance*/

        /*foreach (var question in openQuestions)
        {
            var statistic = new Statistic();
            statistic.QuestionText = question.Text;
            var openAnswers = answers.Where(a => a.OpenQuestion.Id == question.Id);
            foreach (var answer in openAnswers)
            {
                if (statistic.Answers.ContainsKey(answer.ChosenAnswer))
                {
                    statistic.Answers[answer.ChosenAnswer]++;
                }
                else
                {
                    statistic.Answers.Add(answer.ChosenAnswer, 1);
                }
            }
            stats.Add(statistic);
        }*/
        return stats;
    }
}

    
    
