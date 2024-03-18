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
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetSingleChoiceQuestionsOfFlow(long flowId)
    {
        var scq = _flowManager.GetSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
    
        if (!scq.Any())
        {
            return NoContent();
        }
    
        return Ok(scq.Select(scq => new SingleChoiceQuestionDto()
        {
            Text = scq.Text,
            SequenceNumber = scq.SequenceNumber,
            Active = scq.Active,
            Options = scq.Options.Select(option => option.OptionText).ToList()
        }));
    }
    
    [HttpGet("{flowId}/MultipleChoiceQuestions")]
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetMultipleChoiceQuestionsOfFlow(long flowId)
    {
        var mcq = _flowManager.GetMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
    
        if (!mcq.Any())
        {
            return NoContent();
        }
    
        return Ok(mcq.Select(mcq => new MultipleChoiceQuestionDto()
        {
            Text = mcq.Text,
            SequenceNumber = mcq.SequenceNumber,
            Active = mcq.Active,
            Options = mcq.Options.Select(option => option.OptionText).ToList()
        }));
    }
    
    [HttpGet("{flowId}/RangeQuestions")]
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetRangeQuestionsOfFlow(long flowId)
    {
        var rq = _flowManager.GetRangeQuestionsWithOptionsOfFlowById(flowId);
    
        if (!rq.Any())
        {
            return NoContent();
        }
    
        return Ok(rq.Select(rq => new RangeQuestionDto()
        {
            Text = rq.Text,
            SequenceNumber = rq.SequenceNumber,
            Active = rq.Active,
            Options = rq.Options.Select(option => option.OptionText).ToList()
        }));
    }
    
    [HttpGet("{flowId}/OpenQuestions")]
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetOpenQuestionsOfFlow(long flowId)
    {
        var oq = _flowManager.GetOpenQuestionsWithAnswerOfFlowById(flowId);
    
        if (!oq.Any())
        {
            return NoContent();
        }
    
        return Ok(oq.Select(oq => new OpenQuestionDto()
        {
            Text = oq.Text,
            SequenceNumber = oq.SequenceNumber,
            Active = oq.Active,
            Answer = oq.Answer.Text
        }));
    }
    
    [HttpGet("{flowId}/SubThemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemasFlow(int flowId)
    {
        var flows = _flowManager.GetSubThemasFlow(flowId);

        if (!flows.Any())
        {
            return NoContent();
        }
        return Ok(flows.Select(flow => new SubThemasDto()
        {
            Title = flow.Title,
            Description = flow.Description
        }));
    }

}