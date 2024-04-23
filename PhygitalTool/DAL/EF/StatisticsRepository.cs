using Microsoft.EntityFrameworkCore;
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
        var multipleChoiceQuestions = _flowRepository.ReadMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
        var rangeQuestions = _flowRepository.ReadRangeQuestionsWithOptionsOfFlowById(flowId);
        
        var answers = _dbContext.Answers
            .Where(a => a.Flow.Id == flowId)
            .ToList();

        if (flow == null)
        {
            throw new Exception($"No Flow with ID {flowId} exists.");
        }

        var stats = new List<Statistic>();

        foreach (var question in multipleChoiceQuestions)
        {
            var statistic = new Statistic();
            statistic.QuestionText = question.Text;
            foreach (var option in question.Options)
            {
                
                var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
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
                var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
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
                var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                statistic.Answers.Add(option.OptionText, answerCount);
            }
            stats.Add(statistic);
        }
        
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
        
        
        /*foreach (var question in questions)
        {
            var statistic = new Statistic();

            if (question is MultipleChoice multipleChoiceQuestion)
            {
                statistic.QuestionText = multipleChoiceQuestion.Text;
                // process multiple choice question
                foreach (var option in multipleChoiceQuestion.Options)
                {
                    var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                    statistic.Answers.Add(option.OptionText, answerCount);
                }
            }
            else if (question is SingleChoiceQuestion singleChoiceQuestion)
            {
                statistic.QuestionText = singleChoiceQuestion.Text;
                // process single choice question
                foreach (var option in singleChoiceQuestion.Options)
                {
                    var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                    statistic.Answers.Add(option.OptionText, answerCount);
                }
            }
            else if (question is RangeQuestion rangeQuestion)
            {
                statistic.QuestionText = rangeQuestion.Text;
                // process range question
                foreach (var option in rangeQuestion.Options) 
                {
                    var answerCount = answers.Count(a => a.ChosenAnswer == option.OptionText);
                    statistic.Answers.Add(option.OptionText, answerCount);
                }
            }
            else if (question is OpenQuestion openQuestion)
            {
                statistic.QuestionText = openQuestion.Text;
                // process open question
                var openAnswers = answers.Where(a => a.OpenQuestion.Id == openQuestion.Id);
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
                
            }

            stats.Add(statistic);
        }*/

        return stats;
    }
}

    
    
