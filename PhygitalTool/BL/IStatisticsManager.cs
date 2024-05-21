using Phygital.Domain.Statistics;

namespace Phygital.BL;

public interface IStatisticsManager
{
    IEnumerable<Statistic> GetFlowStatistics(long flowId);
    IEnumerable<Statistic> GetFlowParticipationsStatistics(long flowId);
    Dictionary<string, int> GetParticipationCountsByTimeSpentCategories(long flowId);
}