using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.Domain.Questionsprocess.Questions;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

// API Controller for all question pages
[ApiController]
[Route("/api/[controller]")]
public class QuestionsController : Controller
{
    private readonly IFlowElementManager _flowElementManager;
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _unitOfWork;
    private List<Option> optionlist = new List<Option>(); 
    
    public QuestionsController(IFlowManager flowManager, IFlowElementManager flowElementManager,
        IThemeManager themeManager, IAnswerManager answerManager, UnitOfWork unitOfWork)
    {
        _flowElementManager = flowElementManager;
        _themeManager = themeManager;
        _unitOfWork = unitOfWork;
    }

    
    // Deze methode geprobeerd met optiondto maar het kon niet parsen naar hier
    [HttpPost("SaveOptions")]
    public IActionResult SaveOptions([FromBody] List<String> options)
    {
        foreach (var option in options)
        {
            Option optionToAdd = new Option
            {
                OptionText = option
            };
            optionlist.Add(optionToAdd);
            _unitOfWork.BeginTransaction();
            _flowElementManager.AddOption(optionToAdd);
            _unitOfWork.Commit();
        }
        return Ok();
    }
    
    [HttpPost("AddQuestion")]
    public ActionResult PostQuestion([FromBody] QuestionDto questionDto)
    {
        switch (questionDto.Type.ToLower())
        {
            case "open":
                OpenQuestion openQuestionToAdd = new OpenQuestion
                {
                    Text = questionDto.Text,
                    Active = questionDto.isActive,
                    Theme = _themeManager.GetThemeById(questionDto.SubTheme),
                };

                // Add question to database
                _unitOfWork.BeginTransaction();
                _flowElementManager.AddOpenQuestion(openQuestionToAdd);
                _unitOfWork.Commit();
                break;

            case "multiplechoice":
                MultipleChoice multipleChoice = new MultipleChoice
                {
                    Text = questionDto.Text,
                    Active = questionDto.isActive,
                    Theme = _themeManager.GetThemeById(questionDto.SubTheme),
                    Options = optionlist
                };
                _unitOfWork.BeginTransaction();
                _flowElementManager.AddMultipleChoiceQuestion(multipleChoice);
                _unitOfWork.Commit();
                break;

            case "singlechoice":
                SingleChoiceQuestion singleChoice = new SingleChoiceQuestion
                {
                    Text = questionDto.Text,
                    Active = questionDto.isActive,
                    Theme = _themeManager.GetThemeById(questionDto.SubTheme),
                    Options = optionlist
                };
                _unitOfWork.BeginTransaction();
                _flowElementManager.AddSingleChoiceQuestion(singleChoice);
                _unitOfWork.Commit();
                break;

            case "range":
                RangeQuestion range = new RangeQuestion
                {
                    Text = questionDto.Text,
                    Active = questionDto.isActive,
                    Theme = _themeManager.GetThemeById(questionDto.SubTheme),
                    Options = optionlist
                };
                _unitOfWork.BeginTransaction();
                _flowElementManager.AddRangeQuestion(range);
                _unitOfWork.Commit();
                break;
        }
        optionlist.Clear();
        return Ok();
    }
}