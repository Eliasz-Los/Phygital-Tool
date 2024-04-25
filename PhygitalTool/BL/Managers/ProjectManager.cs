using Phygital.DAL;
using Phygital.Domain.Subplatform;

namespace Phygital.BL.Managers;

public class ProjectManager : IProjectManager
{
    private readonly IProjectRepository _projectRepository;

    public ProjectManager(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public void AddProject(Project project)
    {
        _projectRepository.CreateProject(project);
    }
}