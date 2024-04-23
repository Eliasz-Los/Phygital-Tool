using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain.Datatypes;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.Domain.Subplatform;
using Phygital.Domain.Themas;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IThemeManager _themeManager;
    private readonly IProjectManager _projectManager;
    private readonly UnitOfWork _unitOfWork;

    public ProjectsController(IThemeManager themeManager, IProjectManager projectManager, UnitOfWork unitOfWork)
    {
        _themeManager = themeManager;
        _projectManager = projectManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("subthemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemas()
    {
        var subthemas = _themeManager.GetAllSubThemas();

        if (!subthemas.Any())
        {
            return NoContent();
        }
        return Ok(subthemas.Select(subthema => new SubThemasDto()
        {
            Title = subthema.Title,
            Description = subthema.Description
        }));
    }
    
    
    // ID meegeven in javascript
    [HttpPost("AddProject")]
    public ActionResult PostProject([FromBody] ProjectCreationModel model)
    {
        string name = model.Name;
        SubThemasDto mainTheme = model.MainTheme;
        List<SubThemasDto> themas = model.Themas;
        
        Theme theme = new Theme
        {
            Title = mainTheme.Title,
            Description = mainTheme.Description,
            SubThemas = new List<Theme>()
        };

        // We make a dummy flow to put into the project which contains the main theme and the subthemes (can be edited later by the user)
        Flow flow = new Flow
        {
            Theme = theme,
            FlowType = Flowtype.linear
        };

        // Placeholder for subthemes
        Theme hulp = new Theme();
        
        // Add subthemes to the main theme
        foreach (SubThemasDto subthema in themas)
        {
            hulp.Title = subthema.Title;
            hulp.Description = subthema.Description;
            theme.SubThemas.Add(hulp);
        }

        // Make the project
        Project project = new Project
        {
            Name = name,
            Flows = new List<Flow>()
        };
        
        // Add standard flow to project
        _unitOfWork.BeginTransaction();
        project.Flows.Add(flow);
        _projectManager.AddProject(project);
        _unitOfWork.Commit();
        return Ok();
    }
    
}