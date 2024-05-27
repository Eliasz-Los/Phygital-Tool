using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Statistics;

namespace Phygital.DAL.EF;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly PhygitalDbContext _dbContext;

    public StatisticsRepository(PhygitalDbContext dbContext)
    {
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
            .Where(a => a.Flow.Id == flowId && (a.MultipleChoice != null || a.SingleChoiceQuestion != null ||
                                                a.RangeQuestion != null || a.OpenQuestion != null))
            .ToList();

        // Alle antwoorden groepen op de vraag zelf
        var groupedAnswers = answers.GroupBy(a =>
                a.MultipleChoice?.Text ?? a.SingleChoiceQuestion?.Text ?? a.RangeQuestion?.Text ?? a.OpenQuestion?.Text)
            .Distinct();

        List<Statistic> statistics = new List<Statistic>();

        // Elke groep antwoorden doorlopen
        foreach (var group in groupedAnswers)
        {
            // Hier per groep de statistiek opbouwen dus per vraag
            var stat = new Statistic();
            stat.Title = group.Key;

            foreach (var answer in group)
            {
                if (answer.MultipleChoice != null || answer.SingleChoiceQuestion != null ||
                    answer.RangeQuestion != null)
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

    public IEnumerable<Statistic> GetFlowParticipantsStatistics(long flowId)
    {
        var participations = _dbContext.Participations.Include(p => p.Flow).ToList();

        var groupedParticipations = participations.GroupBy(p => p.Flow.Id)
            .Select(group => new Statistic
            {
                Title = $"Flow ID: {group.Key}",
                Participations = new Dictionary<string, int> { { "Participation Count", group.Count() } }
            });

        return groupedParticipations;
    }
    
    public Dictionary<string, int> GetParticipationCountsByTimeSpentCategories(long flowId)
    {
        var participations = _dbContext.Participations
            .Where(p => p.Flow.Id == flowId)
            .ToList();

        var categories = new Dictionary<string, int>
        {
            {"< 1 min", 0},
            {"1-5 min", 0},
            {"5-10 min", 0},
            {"10-30 min", 0},
            {"> 30 min", 0}
        };

        foreach (var participation in participations)
        {
            var durationInMinutes = (participation.EndTime - participation.StartTime).TotalMinutes;

            if (durationInMinutes < 1)
            {
                categories["< 1 min"]++;
            }
            else if (durationInMinutes <= 5)
            {
                categories["1-5 min"]++;
            }
            else if (durationInMinutes <= 10)
            {
                categories["5-10 min"]++;
            }
            else if (durationInMinutes <= 30)
            {
                categories["10-30 min"]++;
            }
            else
            {
                categories["> 30 min"]++;
            }
        }

        return categories;
    }
}