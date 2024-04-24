namespace Phygital.DAL.EF;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FeedbackRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}