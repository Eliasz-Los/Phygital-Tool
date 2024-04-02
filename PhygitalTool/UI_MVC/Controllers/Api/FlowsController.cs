using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class FlowsController : ControllerBase
{
    private readonly IFlowManager _flowManager;
    private readonly UnitOfWork _unitOfWork;
    
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
    public ActionResult<IEnumerable<MultipleChoiceQuestionDto>> GetMultipleChoiceQuestionsOfFlow(long flowId)
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
    public ActionResult<IEnumerable<RangeQuestionDto>> GetRangeQuestionsOfFlow(long flowId)
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
    public ActionResult<IEnumerable<OpenQuestionDto>> GetOpenQuestionsOfFlow(long flowId)
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
            Answer = oq.Answer.ToString()
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

    [HttpPost("{flowId}/AddAnswers")]
    public ActionResult PostAnswers(long flowId, [FromBody] List<AnswerDto> answers)
    {
        var flow = _flowManager.GetFlowById(flowId);

        if (flow == null)
        {
            return NotFound("Flow not found");
        }

        if (!answers.Any())
        {
            return NoContent();
        }

        foreach (var answerDto in answers)
        {
            if (answerDto.ChosenAnswer == null || !answerDto.ChosenOptions.Any() || answerDto.SubTheme == null)
            {
                return BadRequest("Invalid answer(s) or Subtheme");
            }

            var answer = new AnswerDto()
            {
                Flow = flow,
                ChosenOptions = answerDto.ChosenOptions.Select(option =>
                {
                    var optionEnt = _flowManager.GetOptionByText(option.OptionText);

                    return new Option { OptionText = optionEnt.OptionText };
                }).ToList(),
                ChosenAnswer = answerDto.ChosenAnswer,
                SubTheme = answerDto.SubTheme
            };
            
            //eerste effe zien of we kunnen uitkrijgen
          //  _unitOfWork.BeginTransaction();
            _flowManager.AddAnswerToFlow(answer.Flow, answer.ChosenOptions, answer.ChosenAnswer, answer.SubTheme);
            //_unitOfWork.Commit();
        }

        return Ok();

    }

}