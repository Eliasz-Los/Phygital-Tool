using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain.User;
using Phygital.UI_MVC.Models;
using Phygital.UI_MVC.Models.Dto.Feedback;

namespace Phygital.UI_MVC.Controllers;

public class FeedbackController : Controller
{
    private readonly UnitOfWork _uow;
    private readonly IFeedbackManager _feedbackManager;
    private readonly IThemeManager _themeManager;
    private readonly UserManager<Account> _userManager;
    
    public FeedbackController(UnitOfWork uow, IFeedbackManager feedbackManager, IThemeManager themeManager, UserManager<Account> userManager)
    {
        _uow = uow;
        _feedbackManager = feedbackManager;
        _themeManager = themeManager;
        _userManager = userManager;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> Index()
    {
        var posts = await _feedbackManager.GetAllPostsWithReactionsAndLikes();
        var viewModel = new FeedbackViewModel
        {
            Posts = posts,
            Reaction = new ReactionDto()
        };
        return View(viewModel);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public IActionResult Add()
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public IActionResult Add(PostDto postDto)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        if (!ModelState.IsValid)
            return View();

        var  currentAccount = _userManager.FindByNameAsync(User.Identity.Name).Result;
        if (currentAccount == null)
        {
            return View("Error", new ErrorViewModel { RequestId = "Account not found" });
        }
     
        _uow.BeginTransaction();
        _feedbackManager.AddPost(postDto.Title, postDto.Text, postDto.ThemeId, currentAccount);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> Edit(long id)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        
        var post = await _feedbackManager.GetPostWithThemeByIdAsync(id);
        var postDto = new PostDto
        {
            Title = post.Title,
            Text = post.Text,
            ThemeId = post.Theme.Id
        };
        return View(postDto);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> Edit(long id, PostDto postDto)
    {
        if (!ModelState.IsValid)
            return View();
        var theme = _themeManager.GetThemeById(postDto.ThemeId);
        
        _uow.BeginTransaction();
        await _feedbackManager.ChangePost(id, postDto.Title, postDto.Text, theme.Id);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _feedbackManager.RemovePost(id);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    
    [HttpPost]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async  Task<IActionResult> LikePost(long postId)
    {
        var  currentAccount = _userManager.FindByNameAsync(User.Identity.Name).Result;
        
        _uow.BeginTransaction();
        
        
        var result = await _feedbackManager.AddPostLikeByPostId(postId, currentAccount);
        _uow.Commit();
        
        if (result == null)
        {
            ModelState.AddModelError("LikeError", "Je hebt deze post al geliked");
            var posts = await _feedbackManager.GetAllPostsWithReactionsAndLikes();
            var viewModel = new FeedbackViewModel
            {
                Posts = posts,
                Reaction = new ReactionDto()
            };
            return View("Index", viewModel);
        }

        return RedirectToAction("Index", "Feedback");
        
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> DislikePost(long postId)
    {
        var  currentAccount = _userManager.FindByNameAsync(User.Identity.Name).Result;
        
        _uow.BeginTransaction();
       var result = await _feedbackManager.AddDislikePostByPostId(postId, currentAccount);
        _uow.Commit();
        
        if (result == null)
        {
            ModelState.AddModelError("DislikeError", "Je hebt deze post al gedisliked");
            //zodat de errors niet verloren gaan
            var posts = await _feedbackManager.GetAllPostsWithReactionsAndLikes();
            var viewModel = new FeedbackViewModel
            {
                Posts = posts,
                Reaction = new ReactionDto()
            };
            return View("Index", viewModel);
        }
        return RedirectToAction("Index", "Feedback");
    }
    
    //Misschien is het beter om in aparte controller te zetten: api/feedback/{postId}/AddReaction
    [HttpPost("{postId}/AddReaction")]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> AddReactionToPost(long postId, ReactionDto reactionDto)
    {
        if(!ModelState.IsValid)
            return RedirectToAction("Index", "Feedback");
        
        var  currentAccount = _userManager.FindByNameAsync(User.Identity.Name).Result;
        
        _uow.BeginTransaction();
        await _feedbackManager.AddReactionToPostById(postId, reactionDto.Content, currentAccount);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
}