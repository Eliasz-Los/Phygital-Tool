using BL;
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
    private readonly IFlowManager _flowManager;
    
    public ProjectsController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }
    
    [HttpGet("subthemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemas()
    {
        var subthemas = _flowManager.GetAllSubThemas();

        if (!subthemas.Any())
        {
            return NoContent();
        }
        return Ok(subthemas.Select(flow => new SubThemasDto()
        {
            Title = flow.Title,
            Description = flow.Description
        }));
    }
    
    [HttpPost("AddProject")]
    public ActionResult PostProject([FromBody] ProjectCreationModel model)
    {
        string name = model.Name;
        SubThemasDto mainTheme = model.MainTheme;
        List<SubThemasDto> themas = model.Themas;
        
        // ThemeDTO convert to thema (mogelijks belachelijk)
        Theme theme = new Theme
        {
            Title = mainTheme.Title,
            Description = mainTheme.Description
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
            Name = name
        };
        
        // Add standard flow to project
        project.Flows.Add(flow);
        
        _flowManager.AddProject(project);
        return Ok();
    }
    
}