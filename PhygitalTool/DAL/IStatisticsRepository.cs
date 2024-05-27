using Phygital.Domain.Statistics;

namespace Phygital.DAL;

public interface IStatisticsRepository
{
    IEnumerable<Statistic> GetFlowStatistics(long flowId);
    IEnumerable<Statistic> GetFlowParticipantsStatistics(long flowId);
    Dictionary<string, int> GetParticipationCountsByTimeSpentCategories(long flowId);
}