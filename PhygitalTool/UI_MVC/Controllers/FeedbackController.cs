using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.UI_MVC.Models.Dto.Feedback;

namespace Phygital.UI_MVC.Controllers;

public class FeedbackController : Controller
{
    private readonly UnitOfWork _uow;
    private readonly IFeedbackManager _feedbackManager;
    
    public FeedbackController(UnitOfWork uow, IFeedbackManager feedbackManager)
    {
        _uow = uow;
        _feedbackManager = feedbackManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var posts = await _feedbackManager.GetAllPostsWithReactionsAndLikes();
        return View(posts);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Add(PostDto postDto)
    {
        if(!ModelState.IsValid)
            return View();
        
       
        _uow.BeginTransaction();
        _feedbackManager.AddPost(postDto.Title, postDto.Text);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var post = await _feedbackManager.GetPostByIdAsync(id);
        return View(post);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(long id, PostDto postDto)
    {
        if (!ModelState.IsValid)
            return View();
        _uow.BeginTransaction();
        await _feedbackManager.UpdatePost(id, postDto.Title, postDto.Text);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpPost]
    public IActionResult Delete(long id)
    {
        _uow.BeginTransaction();
        _feedbackManager.DeletePost(id);
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
    public async Task<IActionResult> DislikePost(long postId, long likeId)
    {
        _uow.BeginTransaction();
        await _feedbackManager.RemovePostLikeByPostId(postId, likeId);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
    
    [HttpPost("{postId}/AddReaction")]
    public async Task<IActionResult> AddReaction(long postId, string content)
    {
        _uow.BeginTransaction();
        await _feedbackManager.AddReactionToPostById(postId, content);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
}