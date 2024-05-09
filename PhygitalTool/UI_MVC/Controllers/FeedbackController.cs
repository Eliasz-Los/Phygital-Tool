using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.UI_MVC.Models.Dto.Feedback;

namespace Phygital.UI_MVC.Controllers;

public class FeedbackController : Controller
{
    private readonly UnitOfWork _uow;
    private readonly IFeedbackManager _feedbackManager;
    private readonly IThemeManager _themeManager;
    
    public FeedbackController(UnitOfWork uow, IFeedbackManager feedbackManager, IThemeManager themeManager)
    {
        _uow = uow;
        _feedbackManager = feedbackManager;
        _themeManager = themeManager;
    }
    
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
    public IActionResult Add()
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        return View();
    }
    
    [HttpPost]
    public IActionResult Add(PostDto postDto)
    {
        if(!ModelState.IsValid)
            return View();
        
       
        _uow.BeginTransaction();
        _feedbackManager.AddPost(postDto.Title, postDto.Text, postDto.ThemeId);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpGet]
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
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _feedbackManager.RemovePost(id);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    
    [HttpPost]
    public async  Task<IActionResult> LikePost(long postId)
    {
        _uow.BeginTransaction();
        await _feedbackManager.AddPostLikeByPostId(postId);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpPost]
    public async Task<IActionResult> DislikePost(long postId)
    {
        _uow.BeginTransaction();
        await _feedbackManager.AddDislikePostByPostId(postId);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    //Misschien is het beter om in aparte controller te zetten: api/feedback/{postId}/AddReaction
    [HttpPost("{postId}/AddReaction")]
    public async Task<IActionResult> AddReaction(long postId, ReactionDto reactionDto)
    {
        if(!ModelState.IsValid)
            return RedirectToAction("Index", "Feedback");
        
        
        _uow.BeginTransaction();
        await _feedbackManager.AddReactionToPostById(postId, reactionDto.Content);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
}