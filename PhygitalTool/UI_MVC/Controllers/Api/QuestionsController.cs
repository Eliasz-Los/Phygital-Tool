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

    public QuestionsController(IFlowManager flowManager, IFlowElementManager flowElementManager, IThemeManager themeManager, IAnswerManager answerManager, UnitOfWork unitOfWork)
    {
        _flowElementManager = flowElementManager;
        _themeManager = themeManager;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("AddOpenQuestion")]
    public ActionResult PostOpenQuestion([FromBody] QuestionDto questionDto)
    {
        
        OpenQuestion openQuestionToAdd = new OpenQuestion
        {
            Text = questionDto.Text,
            Active =questionDto.isActive,
            Theme = _themeManager.GetThemeById(questionDto.SubTheme)
        };

        // Add flow to database
        _unitOfWork.BeginTransaction();
        _flowElementManager.AddOpenQuestion(openQuestionToAdd);
        _unitOfWork.Commit();
        return Ok();
    }

}