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
    // Retrieve all answers for the specified flow
    var answers = _dbContext.Answers
        .Include(a => a.ChosenOptions)
        .Include(a => a.OpenQuestion)
        .Include(a => a.MultipleChoice)
        .Include(a => a.RangeQuestion)
        .Include(a => a.SingleChoiceQuestion)
        .Where(a => a.Flow.Id == flowId && (a.MultipleChoice != null || a.SingleChoiceQuestion != null || a.RangeQuestion != null || a.OpenQuestion != null))
        .ToList();

    var groupedAnswers = answers.GroupBy(a => a.MultipleChoice?.Text ?? a.SingleChoiceQuestion?.Text ?? a.RangeQuestion?.Text ?? a.OpenQuestion?.Text);
    
    // Initialize a list to hold the statistics
    List<Statistic> statistics = new List<Statistic>();

    // Iterate over each group of answers
    foreach (var group in groupedAnswers)
    {
        // Initialize a new statistic for this group
        var stat = new Statistic();
        stat.QuestionText = group.Key;
        
        foreach (var answer in group)
        {
            /*// Initialize a new statistic for this answer
            Statistic statistic = new Statistic
            {
                QuestionText = answer.OpenQuestion?.Text ?? answer.MultipleChoice?.Text ?? answer.RangeQuestion?.Text ?? answer.SingleChoiceQuestion?.Text
            };*/

            // If the answer is a multiple choice or single choice question, count the chosen options
            if (answer.MultipleChoice != null || answer.SingleChoiceQuestion != null || answer.RangeQuestion != null)
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
            // If the answer is an open question or range question, count the chosen answer
            else if (answer.OpenQuestion != null)
            {
                if (stat.Answers.ContainsKey(answer.ChosenAnswer))
                {
                    stat.Answers[answer.ChosenAnswer]++;
                }
                else
                {
                    stat.Answers[answer.ChosenAnswer] = 1;
                }
            }

            // Add the statistic to the list
            statistics.Add(stat);
        }

    }
    // Iterate over each answer
    
    // Return the list of statistics
    return statistics;
}

}


//TODO: the beginning of the method
/*   var flow = _flowRepository.ReadFlowById(flowId);
        
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
        
        
        return stats;*/
     //TODO: works the best
        /*//group answers by question
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
        }*/
        
        //TODO: works badly
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
        //should work only for open quesitons
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
    
