using Phygital.Domain.Session;

namespace Phygital.DAL.EF;

public class SessionRepository : ISessionRepository
{
    private readonly PhygitalDbContext _dbContext;

    public SessionRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void CreateSession(Session session)
    {
        _dbContext.Sessions.Add(session);
    }
    
    public void CreateParticipation(Participation participation)
    {
        _dbContext.Participations.Add(participation);
    }
    
    public Session ReadSessionById(long id)
    {
        return _dbContext.Sessions.Find(id);
    }
    
    public Installation ReadInstallationById(long id)
    {
        return _dbContext.Installations.Find(id);
    }

    public Participation ReadParticipationById(long id)
    {
        return _dbContext.Participations.Find(id);
    }

    public IEnumerable<Participation> ReadAllParticipations()
    {
        return _dbContext.Participations;
    }

    public void UpdateParticipation(Participation participation)
    {
        _dbContext.Participations.Update(participation);
    }
}