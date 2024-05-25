using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.Domain;
using Phygital.Domain.User;
using Phygital.UI_MVC.Models.Dto.Feedback;
using Phygital.UI_MVC.Services;

namespace Phygital.UI_MVC.Controllers;

public class FeedbackController : Controller
{
    private readonly UnitOfWork _uow;
    private readonly IFeedbackManager _feedbackManager;
    private readonly IThemeManager _themeManager;
    private readonly UserManager<Account> _userManager;
    private readonly ILogger<FeedbackController> _logger;
    private readonly ICloudStorage _cloudStorage;
    
    public FeedbackController(UnitOfWork uow, IFeedbackManager feedbackManager, IThemeManager themeManager, UserManager<Account> userManager, ILogger<FeedbackController> logger, ICloudStorage cloudStorage)
    {
        _uow = uow;
        _feedbackManager = feedbackManager;
        _themeManager = themeManager;
        _userManager = userManager;
        _logger = logger;
        _cloudStorage = cloudStorage;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> Index()
    {
        var posts = await _feedbackManager.GetAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
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
    public async Task<IActionResult> Add(PostDto postDto)
    {
        var themes = _themeManager.GetAllThemas();
        ViewBag.Themes = themes;
        if (!ModelState.IsValid)
            return View();

        Account currentAccount = new Account();
        if (User.Identity?.Name != null)
        {
            currentAccount =  _userManager.FindByNameAsync(User.Identity.Name).Result;
        }
     
        _uow.BeginTransaction();
        await _feedbackManager.AddPost(postDto.Title, postDto.Text, postDto.ThemeId, currentAccount, postDto.ImageFile);
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
        Account user = new Account();
        if (User.Identity?.Name != null)
        {
            user = await _userManager.FindByNameAsync(User.Identity.Name);
        }
        
        if (post.Account.UserName != user?.UserName)
            return Forbid();
        
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
    [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
    public async Task<IActionResult> Delete(long id)
    {
        Account user = new Account();
        if (User.Identity?.Name != null)
        {
            user = await _userManager.FindByNameAsync(User.Identity.Name);
        }
        
        var post = await _feedbackManager.GetPostWithThemeByIdAsync(id);
        
        if (post == null)
            return NotFound();
        
        if (user == null)
            return Challenge();
        
        if (post.Account.UserName != user.UserName && !User.IsInRole("Admin") && !User.IsInRole("SubAdmin"))
        {
            _logger.LogInformation("Unauthorized user, {user} : {post}", user.UserName, post.Account.UserName);
            return Forbid();
        }
        
        _uow.BeginTransaction();
         _feedbackManager.RemovePost(id);
        _uow.Commit();
        return RedirectToAction("Index", "Feedback");
    }
}