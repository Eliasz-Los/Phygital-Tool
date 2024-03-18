using Microsoft.AspNetCore.Mvc;
using Phygital.BL;

namespace Phygital.UI_MVC.Controllers;

public class OpenQuestionController: Controller
{
    private readonly ILogger<HomeController> _logger;
    /*
    private readonly IOpenQuestionManager _flowManager;
    */
    private readonly UnitOfWork _uow;

    // TODO manager moet hier nog in
    public OpenQuestionController(ILogger<HomeController> logger, UnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    public IActionResult Index()
    {
        //var OpenQuestion = _flowManager.GetOpenQuestion();
        // TODO question meegeven zodat deze gedisplayed kan worden
        return View();
    }

}