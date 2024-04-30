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

    // Group the answers by question text so that it can be added by each distinct question
    var groupedAnswers = answers.GroupBy(a => a.MultipleChoice?.Text ?? a.SingleChoiceQuestion?.Text ?? a.RangeQuestion?.Text ?? a.OpenQuestion?.Text).Distinct();
    
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
            // If the answer is a multiple, range or single question, count the chosen options
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

            
        }
        // Add the statistic to the list
        statistics.Add(stat);
    }
    // Return the list of statistics
    return statistics;
}

}
    
