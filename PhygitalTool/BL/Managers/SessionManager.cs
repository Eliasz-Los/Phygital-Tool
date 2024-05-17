using Phygital.DAL;
using Phygital.Domain.Session;

namespace Phygital.BL.Managers;

public class SessionManager : ISessionManager
{
    private readonly ISessionRepository _sessionRepository;

    public SessionManager(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    
    public void AddSession(Session session)
    {
        _sessionRepository.CreateSession(session);
    }
    
    public void AddParticipation(Participation participation)
    {
        _sessionRepository.CreateParticipation(participation);
    }
    
    public Session GetSessionById(long id)
    {
        return _sessionRepository.ReadSessionById(id);
    }
    
    public Installation GetInstallationById(long id)
    {
        return _sessionRepository.ReadInstallationById(id);
    }
    
    public Participation GetParticipationById(long id)
    {
        return _sessionRepository.ReadParticipationById(id);
    }

    public IEnumerable<Participation> GetAllParticipations()
    {
        return _sessionRepository.ReadAllParticipations();
    }

    public void ChangeParticipation(long id, DateTime StartTime, DateTime EndTime)
    {
        var participation = _sessionRepository.ReadParticipationById(id);
        participation.StartTime = StartTime;
        participation.EndTime = EndTime;
        _sessionRepository.UpdateParticipation(participation);
    }
}