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
    
    public IActionResult ProcessInput(string userInput)
    {
        // When the user presses the button on the page, this method is called and will send the text the user has inputted
        // to the database as an answer
        
        // TODO BL code non existent, maak pls
    
        // After the answer has been processed we redirect the user to the next element in the flow
        // TODO redirect naar ander flowelement
        return RedirectToAction(""); // Redirect to a success page after processing
    }

}