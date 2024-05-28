using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers;


public class QuestionController : Controller
{
    private readonly IFlowManager _flowManager;
    private readonly IFlowElementManager _flowElementManager;
    private readonly UnitOfWork _uow;
    public QuestionController(IFlowManager flowManager, IFlowElementManager flowElementManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _flowElementManager = flowElementManager;
        _uow = unitOfWork;
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        List<QuestionDto> questions = new List<QuestionDto>();

        foreach (var question in _flowElementManager.getQuestionsByFlowId(id))
        {
            var questionToAdd = new QuestionDto();
            questionToAdd.Id = question.Id;
            questionToAdd.SubTheme = question.SubTheme?.Id ?? 1;
            questionToAdd.Text = question.Text;
            questionToAdd.isActive = question.Active;
<<<<<<< HEAD
            questionToAdd.Type = "Open";
=======
            // TODO ookal is het gedelete wordt het nogsteeds opgehaald
>>>>>>> 4426f833011f5411f818753aaa4e9141a4514002
            questionToAdd.FlowId = id;
            questions.Add(questionToAdd);
        }
        ViewBag.SharedFlowId = id;
        IEnumerable<QuestionDto> questionDtos = questions;
        return View(questionDtos);
    }


    [HttpGet("Creation/AddQuestion")]
    public IActionResult GetQuestion(long id)
    {
        FlowDto model = new FlowDto
        {
            Id = id
        };
        
        if (model.Id == 0)
        {
            model.Id = _flowManager.GetAllFlows().Count() + 1;
        }
        return View("Creation/AddQuestion", model);
    }

    [HttpPost]
    public IActionResult AddQuestion()
    {
        return View("Creation/AddQuestion");
    }

    [HttpPost]
    public IActionResult Delete(long questionId, string questionType)
    {
        switch (questionType)
        {
            case "Open":
                _uow.BeginTransaction();
                var delete = _flowElementManager.getOpenQuestionById(questionId);
                _flowElementManager.RemoveOpenQuestionFromFlow(delete.Id);
                _uow.Commit();
                break;
        }
        return RedirectToAction("Index", "Flow");
    }
    
    [HttpPost]
    public IActionResult UpdateActive(long questionId, long questionFlowId)
    {
<<<<<<< HEAD
        switch (questionType)
        {
            case "Open":
                _uow.BeginTransaction();
                _flowElementManager.UpdateActive(questionId);
                _uow.Commit();
                break;
        }
        return RedirectToAction("Edit", "Question", flowId);
=======
        _uow.BeginTransaction();
        _flowElementManager.UpdateActive(questionId);
        _uow.Commit();
        
        return RedirectToAction("Edit", "Question", new { id = questionFlowId });
>>>>>>> 4426f833011f5411f818753aaa4e9141a4514002
    }
}