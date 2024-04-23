using Phygital.DAL;

namespace Phygital.BL.Managers;

public class SessionManager : ISessionManager
{
    private readonly ISessionRepository _sessionRepository;

    public SessionManager(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
}