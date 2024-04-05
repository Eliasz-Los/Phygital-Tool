using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
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
    
    [HttpGet("{flowId}/SubThemas")]
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



}