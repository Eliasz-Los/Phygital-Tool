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
    void ChangeParticipation(long id, DateTime StartTime, DateTime EndTime);
}