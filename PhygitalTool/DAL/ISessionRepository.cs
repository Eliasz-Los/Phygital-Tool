using Phygital.Domain.Session;

namespace Phygital.DAL;

public interface ISessionRepository
{
    void CreateSession(Session session);
    void CreateParticipation(Participation participation);
    Session ReadSessionById(long id);
    Installation ReadInstallationById(long id);
    Participation ReadParticipationById(long id);
    IEnumerable<Participation> ReadAllParticipations();
    
    void UpdateParticipation(Participation participation);
}