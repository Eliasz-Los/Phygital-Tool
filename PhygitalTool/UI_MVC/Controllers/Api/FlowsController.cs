using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.UI_MVC.Models.Dto;
using Phygital.UI_MVC.Models.Dto.Infos;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class FlowsController : ControllerBase
{
    private readonly IFlowManager _flowManager;
    private readonly UnitOfWork _unitOfWork;
    
    public FlowsController(IFlowManager flowManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _unitOfWork = unitOfWork;
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
            Id = scq.Id,
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
            Id = mcq.Id,
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
            Id = rq.Id,
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
            Id = oq.Id,
            Text = oq.Text,
            SequenceNumber = oq.SequenceNumber,
            Active = oq.Active,
            Answer = oq.Answer.ToString()
        }));
    }
    
    [HttpGet("{flowId}/SubThemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemasFlow(long flowId)
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

    [HttpGet("{flowId}/TextInfos")]
    public ActionResult<IEnumerable<TextDto>> GetTextInfosOfFlow(long flowId)
    {
        var texts = _flowManager.GetTextInfosOfFlowById(flowId);

        if (!texts.Any())
        {
            return NoContent();
        }
        return Ok(texts.Select(text => new TextDto()
        {
            Title = text.Title,
            Content = text.Content
        }));
    }
    
    [HttpGet("{flowId}/ImageInfos")]
    public ActionResult<IEnumerable<ImageDto>> GetImageInfosOfFlow(long flowId)
    {
        var images = _flowManager.GetImageInfosOfFlowById(flowId);

        if (!images.Any())
        {
            return NoContent();
        }
        return Ok(images.Select(image => new ImageDto()
        {
            Title = image.Title,
            Url = image.Url,
            AltText = image.AltText
        }));
    }

    [HttpPost("{flowId}/AddAnswers")]
    public ActionResult PostAnswers(long flowId, [FromBody] List<AnswerDto> answers)
    {
        var flow = _flowManager.GetFlowById(flowId);
        var theme = _flowManager.GetSubThemasFlow(flowId);
        
        if (flow == null)
        {
            return NotFound($"Flow with Id: {flowId} not found");
        }

        if (!answers.Any())
        {
            return NoContent();
        }
        
        List<Answer> answerList = new List<Answer>();
        
        foreach (var answerDto in answers)
        {
            var question = _flowManager.GetQuestionById(answerDto.QuestionId);
           
            Answer answer = new Answer
            {
                //kan nog aangepast worden
                Flow = flow,
                SubTheme = theme.Single(),
                ChosenOptions = answerDto.ChosenOptions,
                ChosenAnswer = answerDto.ChosenAnswer
            };
            
            switch (question)
            {
                case OpenQuestion openQuestion:
                    answer.OpenQuestion = openQuestion;
                    break;
                case MultipleChoice multipleChoice:
                    answer.MultipleChoice = multipleChoice;
                    break;
                case RangeQuestion rangeQuestion:
                    answer.RangeQuestion = rangeQuestion;
                    break;
                case SingleChoiceQuestion singleChoiceQuestion:
                    answer.SingleChoiceQuestion = singleChoiceQuestion;
                    break;
            }
            
            answerList.Add(answer);
        }
        
        _unitOfWork.BeginTransaction();
        _flowManager.AddAnswersToFlow(answerList); 
        _unitOfWork.Commit();
        return Ok();
    }

}