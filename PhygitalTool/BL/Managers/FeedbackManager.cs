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

    public async Task<Post> GetPostWithThemeByIdAsync(long id)
    {
        return await _feedbackRepository.ReadPostWithThemeByIdAsync(id);
    }

    public async Task<IEnumerable<Post>> GetAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes()
    {
        return await _feedbackRepository.ReadAllPostsLinkedToAccountWithThemeAndWithReactionsAndLikes();
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

    public async Task<PostLike> GetDislikeByPostIdAndUserId(long postId, string currentAccountId)
    {
        return await _feedbackRepository.ReadDislikeByPostIdAndUserId(postId, currentAccountId);
    }

    public async Task RemovePostDislikeByPostId(long postId, string id)
    {
        await _feedbackRepository.DeletePostDislikeByPostId(postId, id);
    }

    public async Task<PostLike> GetLikeByPostIdAndUserId(long postId, string currentAccountId)
    {
      return await _feedbackRepository.ReadLikeByPostIdAndUserId(postId, currentAccountId);
    }

    public async Task RemovePostLikeByPostId(long postId, string id)
    {
        await _feedbackRepository.DeletePostLikeByPostId(postId, id);
    }

    public async Task<int> GetLikesCountByPostId(long postId)
    {
        return await _feedbackRepository.ReadLikesCountByPostId(postId);
    }

    public async Task<int> GetDislikesCountByPostId(long postId)
    {
        return await  _feedbackRepository.ReadDislikesCountByPostId(postId);
    }

    public async Task<IEnumerable<PostReaction>> GetReactionsOfPostByPostId(long postId)
    {
        return await _feedbackRepository.ReadReactionsOfPostByPostId(postId);
    }
}