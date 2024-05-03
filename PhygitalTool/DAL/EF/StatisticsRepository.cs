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
    var answers = _dbContext.Answers
        .Include(a => a.ChosenOptions)
        .Include(a => a.OpenQuestion)
        .Include(a => a.MultipleChoice)
        .Include(a => a.RangeQuestion)
        .Include(a => a.SingleChoiceQuestion)
        .Where(a => a.Flow.Id == flowId && (a.MultipleChoice != null || a.SingleChoiceQuestion != null || a.RangeQuestion != null || a.OpenQuestion != null))
        .ToList();

    // Alle antwoorden groepen op de vraag zelf
    var groupedAnswers = answers.GroupBy(a => a.MultipleChoice?.Text ?? a.SingleChoiceQuestion?.Text ?? a.RangeQuestion?.Text ?? a.OpenQuestion?.Text).Distinct();
    
    List<Statistic> statistics = new List<Statistic>();

    // Elke groep antwoorden doorlopen
    foreach (var group in groupedAnswers)
    {
        // Hier per groep de statistiek opbouwen dus per vraag
        var stat = new Statistic();
        stat.QuestionText = group.Key;
        
        foreach (var answer in group)
        {
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
        statistics.Add(stat);
    }
    return statistics;
}

}
    
