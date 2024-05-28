using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.Domain.Datatypes;
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
  private readonly ILogger<FeedbacksController> _logger;

  public FeedbacksController(IFeedbackManager feedbackManager, UnitOfWork uow, UserManager<Account> userManager, ILogger<FeedbacksController> logger)
  {
    _feedbackManager = feedbackManager;
    _uow = uow;
    _userManager = userManager;
    _logger = logger;
  }
  
  
  [HttpGet("{postId}/Reactions")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<ActionResult<IEnumerable<ReactionReadDto>>> GetReactions(long postId)
  {
    var reactions = await _feedbackManager.GetReactionsWithAccountAndLikesOfPostByPostIdOrderdByDescPostTime(postId);
    if (!reactions.Any())
    {
      return NoContent();
    }
    
    return Ok(reactions.Select(r => new ReactionReadDto
    {
      Id = r.Id,
      Content = r.Reaction.Content,
      AccountName = r.Reaction.Account.Name,
      LikeCount = r.Reaction.ReactionLikes.Count(rl => rl.Like.LikeType == LikeType.ThumbsUp),
      DislikeCount = r.Reaction.ReactionLikes.Count(rl => rl.Like.LikeType == LikeType.ThumbsDown)
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
      return NotFound($"User {currentAccount.Name} not found");
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    
        
    _uow.BeginTransaction();
    await _feedbackManager.AddReactionToPostById(postId, reactionDto.Content, currentAccount);
    _uow.Commit();

    var reactionsCount =  _feedbackManager.GetReactionsWithAccountAndLikesOfPostByPostIdOrderdByDescPostTime(postId).Result.Count();
    return Ok(reactionsCount);
  }
  
  [HttpDelete("{postId}/DeleteReaction/{reactionId}")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<ActionResult> DeleteReaction(long postId, long reactionId)
  {
    
    Account currentAccount = new Account();
    if (User.Identity?.Name != null)
    {
      currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
    }
    else
    {
      return NotFound($"User {currentAccount.Name} not found");
    }
    var reaction = await _feedbackManager.GetReactionWithAccountById(reactionId);
    
    
    if (reaction.Account.UserName != currentAccount?.UserName && !User.IsInRole("Admin") && !User.IsInRole("SubAdmin"))
    {
      _logger.LogError("Unauthorized user, {user} : {reaction}", currentAccount?.UserName, reaction.Account.UserName);
      return StatusCode(StatusCodes.Status403Forbidden, "Je mag niet andermans reacties verwijderen");
    }
    
    _uow.BeginTransaction();
    await _feedbackManager.RemoveReactionToPostByPostIdAndReactionId(postId, reactionId);
    _uow.Commit();
    return Ok();
  }
 
  
  [HttpPost("{reactionId}/LikeReaction")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<IActionResult> LikeReaction(long reactionId)
  {
      Account currentAccount = new Account();
      if (User.Identity?.Name != null)
      {
          currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
      }        

      _uow.BeginTransaction();
      await _feedbackManager.AddReactionLikeByReactionIdAndAccount(reactionId, currentAccount);
      _uow.Commit();
       
      int likeCount = await _feedbackManager.GetLikesCountByReactionId(reactionId);
      int dislikeCount = await _feedbackManager.GetDislikesCountByReactionId(reactionId);
      
      return Ok( new { likeCount, dislikeCount });
  }
  

  [HttpPost("{reactionId}/DislikeReaction")]
  [Authorize(Roles = "Admin, SubAdmin, Supervisor, User")]
  public async Task<IActionResult> DislikeReaction(long reactionId)
  {
    Account currentAccount = new Account();
    if (User.Identity?.Name != null)
    {
      currentAccount = await _userManager.FindByNameAsync(User.Identity.Name);
    }
    
    _uow.BeginTransaction();
    await _feedbackManager.AddReactionDisLikeByReactionIdAndAccount(reactionId, currentAccount);
    _uow.Commit();
       
    int likeCount = await _feedbackManager.GetLikesCountByReactionId(reactionId);
    int dislikeCount = await _feedbackManager.GetDislikesCountByReactionId(reactionId);
      
    return Ok( new { likeCount, dislikeCount });
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
        
    var existingDislike = await _feedbackManager.GetDislikeByPostIdAndAccountId(postId, currentAccount?.Id);
        
    if (existingDislike != null)
    {
      await _feedbackManager.RemovePostDislikeByPostIdAndAccountId(postId, currentAccount?.Id);
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
        
    var existingDislike = await _feedbackManager.GetLikeByPostIdAndAccountId(postId, currentAccount?.Id);
        
    if (existingDislike != null)
    {
      await _feedbackManager.RemovePostLikeByPostIdAndAccountId(postId, currentAccount?.Id);
    }
    
    await _feedbackManager.AddDislikePostByPostId(postId, currentAccount);
    _uow.Commit();
        
    int dislikeCount = await _feedbackManager.GetDislikesCountByPostId(postId);
    int likeCount = await _feedbackManager.GetLikesCountByPostId(postId);
    return Ok(new { likeCount, dislikeCount});
  }
  
  
}