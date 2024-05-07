using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;

namespace Phygital.UI_MVC.Controllers;

public class FeedbackController : Controller
{
    private readonly UnitOfWork _uow;
    private readonly FeedbackManager _feedbackManager;
    
    public FeedbackController(UnitOfWork uow, FeedbackManager feedbackManager)
    {
        _uow = uow;
        _feedbackManager = feedbackManager;
    }
    
    // GET
    public IActionResult Index()
    {
        var posts = _feedbackManager.GetAllPostsWithReactionsAndLikes();
        return View(posts);
    }
}