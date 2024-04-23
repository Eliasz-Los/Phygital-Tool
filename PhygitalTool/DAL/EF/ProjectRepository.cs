using Phygital.Domain.Subplatform;

namespace Phygital.DAL.EF;

public class ProjectRepository : IProjectRepository
{
    private readonly PhygitalDbContext _dbContext;

    public ProjectRepository(PhygitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void CreateProject(Project project)
    {
        _dbContext.Projects.Add(project);
    }
}