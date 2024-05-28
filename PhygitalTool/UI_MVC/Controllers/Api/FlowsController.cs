using Phygital.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Phygital.BL.Managers;
using Phygital.Domain.Datatypes;
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
    private readonly IFlowElementManager _flowElementManager;
    private readonly IThemeManager _themeManager;
    private readonly IAnswerManager _answerManager;
    private readonly UnitOfWork _unitOfWork;

    public FlowsController(IFlowManager flowManager, IFlowElementManager flowElementManager, IThemeManager themeManager, IAnswerManager answerManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _flowElementManager = flowElementManager;
        _themeManager = themeManager;
        _answerManager = answerManager;
        _unitOfWork = unitOfWork;
    }
    
    
    [HttpGet("{flowId}/SingleChoiceQuestions")]
    public ActionResult<IEnumerable<SingleChoiceQuestionDto>> GetSingleChoiceQuestionsOfFlow(long flowId)
    {
        var scq = _flowElementManager.GetSingleChoiceQuestionsWithOptionsOfFlowById(flowId);
    
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
        var mcq = _flowElementManager.GetMultipleChoiceQuestionsWithOptionsOfFlowById(flowId);
    
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
        var rq = _flowElementManager.GetRangeQuestionsWithOptionsOfFlowById(flowId);
    
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
        var oq = _flowElementManager.GetOpenQuestionsWithAnswerOfFlowById(flowId);
    
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
    
    [HttpGet("{themeId}/OpenQuestionsOfTheme")]
    public ActionResult<IEnumerable<OpenQuestionDto>> GetOpenQuestionsOfTheme(long themeId)
    {
        var oq = _flowElementManager.GetAllOpenQuestionByThemeId(themeId);
    
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
            SubTheme = oq.SubTheme
        }));
    }
    
    [HttpGet("{flowId}/SubThemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemasFlow(long flowId)
    {
        var flows = _themeManager.GetSubThemasFlow(flowId);

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
    
    [HttpPost("AddFlow")]
    // Post method to add a flow to the database with elements retrieved from the page
    public ActionResult PostFlow([FromBody] FlowCreationModel flowModel)
    {
        Flowtype hulp = Flowtype.circular;
        if (flowModel.FlowType == "Linear")
        {
            hulp = Flowtype.linear;
        }
        else
        {
            hulp = Flowtype.circular;
        }
        Flow flowToAdd = new Flow
        {
            FlowType = hulp,
            IsOpen = flowModel.IsOpen,
            Theme = _themeManager.GetThemeById(flowModel.ThemeId)
        };

        // Add flow to database
        _unitOfWork.BeginTransaction();
        _flowManager.AddFlow(flowToAdd);
        _unitOfWork.Commit();
    
        return Ok();
    }

    [HttpGet("{flowId}/TextInfos")]
    public ActionResult<IEnumerable<TextDto>> GetTextInfosOfFlow(long flowId)
    {
        var texts = _flowElementManager.GetTextInfosOfFlowById(flowId);

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
        var images = _flowElementManager.GetImageInfosOfFlowById(flowId);

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
    
    [HttpGet("{flowId}/VideoInfos")]
    public ActionResult<IEnumerable<VideoDto>> GetVideoInfosOfFlow(long flowId)
    {
        var videos = _flowElementManager.GetVideoInfosOfFlowById(flowId);

        if (!videos.Any())
        {
            return NoContent();
        }
        return Ok(videos.Select(video => new VideoDto()
        {
            Title = video.Title,
            Url = video.Url,
            Description = video.Description
        }));
    }

    [HttpPost("{flowId}/AddAnswers")]
    public ActionResult PostAnswers(long flowId, [FromBody] List<AnswerDto> answers)
    {
        var flow = _flowManager.GetFlowById(flowId);
        var theme = _themeManager.GetSubThemasFlow(flowId);
        
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
            var question = _flowElementManager.GetQuestionById(answerDto.QuestionId);
           
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
        _answerManager.AddAnswersToFlow(answerList); 
        _unitOfWork.Commit();
        return Ok();
    }

}