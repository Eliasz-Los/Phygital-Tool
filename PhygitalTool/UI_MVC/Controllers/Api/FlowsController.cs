using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class FlowsController : ControllerBase
{
    private readonly IFlowManager _flowManager;
    
    public FlowsController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }
    
    [HttpGet("{flowId}/SingleChoiceQuestions")]
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetSingleChoiceQuestionsOfFlow(int flowId)
    {
        var flows = _flowManager.GetSingleChoiceQuestionsOfFlow(flowId);
    
        if (!flows.Any())
        {
            return NoContent();
        }
    
        return Ok(flows.Select(flow => new SingleChoiceQuestionDto()
        {
            Text = flow.Text,
            Options = flow.Options
        }));
    }
}