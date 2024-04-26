using Phygital.DAL;
using Phygital.DAL.EF;
using Phygital.Domain.Statistics;

namespace Phygital.BL;

public class StatisticsManager : IStatisticsManager
{
    private readonly IStatisticsRepository _statisticsRepository;
    
    public StatisticsManager(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public IEnumerable<Statistic> GetFlowStatistics(long flowId)
    {
        return _statisticsRepository.GetFlowStatistics(flowId);
    }
}