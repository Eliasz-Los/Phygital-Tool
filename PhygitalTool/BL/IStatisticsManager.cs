using Phygital.Domain.Statistics;

namespace Phygital.BL;

public interface IStatisticsManager
{
    IEnumerable<Statistic> GetFlowStatistics(long flowId);
}