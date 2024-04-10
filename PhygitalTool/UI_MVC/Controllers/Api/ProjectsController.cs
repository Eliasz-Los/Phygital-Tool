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
    public ActionResult PostProject(String name, SubThemasDto mainTheme,[FromBody] List<SubThemasDto> themas)
    {
        // ThemeDTO omzetten naar een theme
        Theme theme = new Theme
        {
            Title = mainTheme.Title,
            Description = mainTheme.Description
        };

        Flow flow = new Flow
        {
            Theme = theme,
            FlowType = Flowtype.linear
        };

        Theme hulp = new Theme();
        foreach (SubThemasDto subthema in themas)
        {
            hulp.Title = subthema.Title;
            hulp.Description = subthema.Description;
            theme.SubThemas.Add(hulp);
        }

        Project project = new Project
        {
            Name = name
        };
        
        
        return Ok();
    }
    
}