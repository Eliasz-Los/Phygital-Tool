namespace Phygital.DAL.EF;

public class PlatformRepository : IPlatformRepository
{
    private readonly PhygitalDbContext _dbContext;

    public PlatformRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}