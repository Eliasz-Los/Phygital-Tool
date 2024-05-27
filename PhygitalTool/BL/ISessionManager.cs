using Phygital.Domain.Session;

namespace Phygital.BL;

public interface ISessionManager
{
    void AddSession(Session session);
    void AddParticipation(Participation participation);
    Session GetSessionById(long id);
    Installation GetInstallationById(long id);
    Participation GetParticipationById(long id);
    IEnumerable<Participation> GetAllParticipations();
    void ChangeParticipation(long id, DateTime startTime, DateTime endTime);
    IEnumerable<Participation> GetParticipationsByFlowId(long flowId);
    int GetTotalParticipationsByFlowId(long flowId);
    TimeSpan GetAverageTimeSpentByFlowId(long flowId);
}