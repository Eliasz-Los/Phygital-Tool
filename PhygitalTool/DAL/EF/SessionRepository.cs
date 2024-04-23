namespace Phygital.DAL.EF;

public class SessionRepository : ISessionRepository
{
    private readonly PhygitalDbContext _dbContext;

    public SessionRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}