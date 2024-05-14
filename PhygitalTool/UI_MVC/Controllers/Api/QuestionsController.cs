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

    public QuestionsController(IFlowManager flowManager, IFlowElementManager flowElementManager,
        IThemeManager themeManager, IAnswerManager answerManager, UnitOfWork unitOfWork)
    {
        _flowElementManager = flowElementManager;
        _themeManager = themeManager;
        _unitOfWork = unitOfWork;
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

        return Ok();
    }
}