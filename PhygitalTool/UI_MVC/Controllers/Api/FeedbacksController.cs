using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Phygital.BL;
using Phygital.Domain;
using Phygital.Domain.User;
using Phygital.UI_MVC.Models.Dto.Feedback;
using Phygital.UI_MVC.Services;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class FeedbacksController : ControllerBase
{
  private readonly IFeedbackManager _feedbackManager;
  private readonly UnitOfWork _uow;
  private readonly UserManager<Account> _userManager;
  private readonly ICloudStorage _cloudStorageService;

  public FeedbacksController(IFeedbackManager feedbackManager, UnitOfWork uow, UserManager<Account> userManager, ICloudStorage cloudStorageService)
  {
    _feedbackManager = feedbackManager;
    _uow = uow;
    _userManager = userManager;
    _cloudStorageService = cloudStorageService;
  }
  
  
  [HttpGet("{postId}/Reactions")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<ActionResult<IEnumerable<ReactionReadDto>>> GetReactions(long postId)
  {
    var reactions = await _feedbackManager.GetReactionsOfPostByPostId(postId);
    if (!reactions.Any())
    {
      return NoContent();
    }
    
    return Ok(reactions.Select(r => new ReactionReadDto
    {
      Content = r.Reaction.Content,
      AccountName = r.Reaction.Account.Name
    }));
  }
  
  [HttpPost("{postId}/AddReaction")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<ActionResult> AddReaction(long postId, [FromBody] ReactionDto reactionDto)
  {
    
    Account currentAccount = new Account();
    if (User.Identity?.Name != null)
    {
      currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
    }
    else
    {
      return NotFound($"Post with id {postId} not found");
    }
    
    if(reactionDto == null)
    {
      return NoContent();
    }
        
    _uow.BeginTransaction();
    await _feedbackManager.AddReactionToPostById(postId, reactionDto.Content, currentAccount);
    _uow.Commit();

    var reactionsCount =  _feedbackManager.GetReactionsOfPostByPostId(postId).Result.Count();
    return Ok(reactionsCount);
  }
  
  [HttpPost("{postId}/LikePost")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async  Task<IActionResult> LikePost(long postId)
  {
    Account currentAccount = new Account();
    if (User.Identity?.Name != null)
    {
      currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
    }        
    _uow.BeginTransaction();
        
    var existingDislike = await _feedbackManager.GetDislikeByPostIdAndUserId(postId, currentAccount?.Id);
        
    if (existingDislike != null)
    {
      await _feedbackManager.RemovePostDislikeByPostId(postId, currentAccount?.Id);
    }
    
    await _feedbackManager.AddPostLikeByPostId(postId, currentAccount);
    _uow.Commit();
     
    int likeCount = await _feedbackManager.GetLikesCountByPostId(postId);
    int dislikeCount = await _feedbackManager.GetDislikesCountByPostId(postId);
    
    return Ok( new { likeCount, dislikeCount });
        
  }
    
  [HttpPost("{postId}/DislikePost")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<IActionResult> DislikePost(long postId)
  {
    Account currentAccount = new Account();
    if (User.Identity?.Name != null)
    {
      currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
    }        
    _uow.BeginTransaction();
        
    var existingDislike = await _feedbackManager.GetLikeByPostIdAndUserId(postId, currentAccount?.Id);
        
    if (existingDislike != null)
    {
      await _feedbackManager.RemovePostLikeByPostId(postId, currentAccount?.Id);
    }
    
    await _feedbackManager.AddDislikePostByPostId(postId, currentAccount);
    _uow.Commit();
        
    int dislikeCount = await _feedbackManager.GetDislikesCountByPostId(postId);
    int likeCount = await _feedbackManager.GetLikesCountByPostId(postId);
    return Ok(new { likeCount, dislikeCount});
  }
  
  
}