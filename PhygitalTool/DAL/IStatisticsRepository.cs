using Phygital.Domain.Statistics;

namespace Phygital.DAL;

public interface IStatisticsRepository
{
    IEnumerable<Statistic> GetFlowStatistics(long flowId);
}