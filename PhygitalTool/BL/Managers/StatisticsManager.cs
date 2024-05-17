using Phygital.DAL;
using Phygital.Domain.Statistics;

namespace Phygital.BL.Managers;

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

    public IEnumerable<Statistic> GetFlowParticipationsStatistics(long flowId)
    {
        return _statisticsRepository.GetFlowParticipantsStatistics(flowId);
    }
}