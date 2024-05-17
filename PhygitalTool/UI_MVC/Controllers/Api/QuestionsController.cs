using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
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
                // TODO: Handle multiple choice questions
                break;

            case "singlechoice":
                // TODO: Handle single choice questions
                break;

            case "range":
                // TODO: Handle range questions
                break;
        }

        optionlist.Clear();
        return Ok();
    }
}