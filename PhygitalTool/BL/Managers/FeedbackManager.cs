using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Phygital.DAL;
using Phygital.Domain.Feedback;
using Phygital.Domain.User;

namespace Phygital.BL.Managers;

public class FeedbackManager : IFeedbackManager
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IThemeManager _themeManager;
    public FeedbackManager(IFeedbackRepository feedbackRepository, IThemeManager themeManager)
    {
        _feedbackRepository = feedbackRepository;
        _themeManager = themeManager;
    }

    public async Task<Post> GetPostWithAccountAndWithThemeById(long id)
    {
        return await _feedbackRepository.ReadPostWithAccountAndWithThemeById(id);
    }

    public async Task<IEnumerable<Post>> GetAllPostsWithAccountWithThemeAndWithReactionsAndLikes()
    {
        return await _feedbackRepository.ReadAllPostsWithAccountWithThemeAndWithReactionsAndLikes();
    }

    public async Task AddPost(string title, string text, long themeId, Account account, IFormFile imageFile)
    {
        var theme = _themeManager.GetThemeById(themeId);
        
        var post = new Post
        {
            Title = title,
            Text = text,
            Theme = theme,
            Account = account,
            ImageFile = imageFile
        };
        Validator.ValidateObject(post, new ValidationContext(post), true);
        await _feedbackRepository.CreatePost(post);
    }


    public async Task ChangePost(long postId, string title, string text, long themeId)
    {
        var post = await _feedbackRepository.ReadPostByIdAsync(postId);
        post.Title = title;
        post.Text = text;

        var theme = _themeManager.GetThemeById(themeId);
        if(theme != null)
            post.Theme = theme;
        
        Validator.ValidateObject(post, new ValidationContext(post), true);
        _feedbackRepository.UpdatePost(post);
    }

    public void RemovePost(long postId)
    {
        _feedbackRepository.DeletePost(postId);
    }

    public async Task<PostLike> AddPostLikeByPostId(long postId, Account account)
    {
        return await _feedbackRepository.LikePost(postId, account);
    }

    public async Task<PostLike> AddDislikePostByPostId(long postId, Account account)
    {
        return await _feedbackRepository.DislikePost(postId, account);
    }
    
    public async Task<Reaction> AddReactionToPostById(long postId, string content, Account account)
    {
        var reaction = new Reaction { Content = content };
        Validator.ValidateObject(reaction, new ValidationContext(reaction), true);
        return await _feedbackRepository.CreateReactionToPostById(postId, reaction, account);
    }

    public async Task<PostLike> GetDislikeByPostIdAndAccountId(long postId, string currentAccountId)
    {
        return await _feedbackRepository.ReadDislikeByPostIdAndAccountId(postId, currentAccountId);
    }

    public async Task RemovePostDislikeByPostIdAndAccountId(long postId, string id)
    {
        await _feedbackRepository.DeletePostLikeByPostIdAndAccountId(postId, id);
    }

    public async Task<PostLike> GetLikeByPostIdAndAccountId(long postId, string currentAccountId)
    {
      return await _feedbackRepository.ReadLikeByPostIdAndAccountId(postId, currentAccountId);
    }

    public async Task RemovePostLikeByPostIdAndAccountId(long postId, string id)
    {
        await _feedbackRepository.DeletePostLikeByPostIdAndAccountId(postId, id);
    }

    public async Task<int> GetLikesCountByPostId(long postId)
    {
        return await _feedbackRepository.ReadLikesCountByPostId(postId);
    }

    public async Task<int> GetDislikesCountByPostId(long postId)
    {
        return await  _feedbackRepository.ReadDislikesCountByPostId(postId);
    }

    public async Task<IEnumerable<PostReaction>> GetReactionsWithAccountAndLikesOfPostByPostId(long postId)
    {
        return await _feedbackRepository.ReadReactionsWithAccountAndLikesOfPostByPostId(postId);
    }

    public async Task RemoveReactionToPostByPostIdAndReactionId(long postId, long reactionId)
    {
         await _feedbackRepository.DeleteReactionToPostByPostIdAndReactionId(postId, reactionId);
    }

    public async Task<Reaction> GetReactionWithAccountById(long reactionId)
    {
        return await _feedbackRepository.ReadReactionWithAccountById(reactionId);
    }

    public async Task<ReactionLike> AddReactionLikeByReactionIdAndAccount(long reactionId, Account currentAccount)
    {
        return await _feedbackRepository.CreateReactionLikeByReactionIdAndAccount(reactionId, currentAccount);
    }

    public async Task<ReactionLike> AddReactionDisLikeByReactionIdAndAccount(long reactionId, Account currentAccount)
    {
        return await _feedbackRepository.CreateReactionDisLikeByReactionIdAndAccount(reactionId, currentAccount);
    }

    public async Task<int> GetLikesCountByReactionId(long reactionId)
    {
        return await _feedbackRepository.ReadLikesCountByReactionId(reactionId);
    }

    public async Task<int> GetDislikesCountByReactionId(long reactionId)
    {
        return await _feedbackRepository.ReadDislikesCountByReactionId(reactionId);
    }
}