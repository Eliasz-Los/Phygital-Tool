using Microsoft.EntityFrameworkCore;
using Phygital.Domain.Feedback;

namespace Phygital.DAL.EF;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly PhygitalDbContext _dbContext;

    public FeedbackRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Post>> ReadAllPostsWithReactionsAndLikes()
    {
        var result = await _dbContext.Posts
            .Include(p => p.PostReactions)
            .Include(p => p.PostLikes)
            .ToListAsync();
        return  result;
    }
}