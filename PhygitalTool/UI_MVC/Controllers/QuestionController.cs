using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;

// This controller is for all the question pages
// TODO als er op add gedrukt wordt komt er een melding succes, maar ook undefined (Typescript error)
public class QuestionController : Controller
{
    private readonly IFlowManager _flowManager;
    private readonly IFlowElementManager _flowElementManager;

    public QuestionController(IFlowManager flowManager, IFlowElementManager flowElementManager)
    {
        _flowManager = flowManager;
        _flowElementManager = flowElementManager;
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        List<QuestionDto> openQuestions = new List<QuestionDto>();

        foreach (var question in _flowElementManager.GetAllOpenQuestionByFlowId(id))
        {
            var questionToAdd = new QuestionDto();
            questionToAdd.Id = question.Id;
            // Todo tijdelijk subtheme default op 1 omdat het crasht
            questionToAdd.SubTheme = question.SubTheme?.Id ?? 1;
            questionToAdd.Text = question.Text;
            questionToAdd.isActive = question.Active;
            questionToAdd.Type = "Open";
            questionToAdd.FlowId = id;
            openQuestions.Add(questionToAdd);
        }

        IEnumerable<QuestionDto> questionDtos = openQuestions;
        return View(questionDtos);
    }


    [HttpGet("Creation/AddQuestion")]
    public IActionResult GetQuestion()
    {
        var highest = _flowManager.GetAllFlows().Count();
        ViewBag.Highest = highest;

        return View("Creation/AddQuestion");
    }

    [HttpPost]
    public IActionResult AddQuestion()
    {
        return View("Creation/AddQuestion");
    }
}