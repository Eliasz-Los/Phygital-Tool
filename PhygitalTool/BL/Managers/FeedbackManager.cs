using System.ComponentModel.DataAnnotations;
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

    public async Task<Post> GetPostByIdAsync(long postId)
    {
        return await _feedbackRepository.ReadPostByIdAsync(postId);
    }

    public async Task<Post> GetPostWithThemeByIdAsync(long id)
    {
        return await _feedbackRepository.ReadPostWithThemeByIdAsync(id);
    }

    public async Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes()
    {
        return await _feedbackRepository.ReadAllPostsWithReactionsAndLikes();
    }

    public void AddPost(string title, string text, long themeId, Account account)
    {
        var theme = _themeManager.GetThemeById(themeId);
        
        var post = new Post
        {
            Title = title,
            Text = text,
            Theme = theme,
            Account = account
        };
        Validator.ValidateObject(post, new ValidationContext(post), true);
        _feedbackRepository.CreatePost(post);
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

    public async Task<PostLike> RemovePostLikeByPostId(long postId, long likeId)
    {
        return await _feedbackRepository.DeletePostLike(postId, likeId);
    }

    public async Task<Reaction> AddReactionToPostById(long postId, string content, Account account)
    {
        var reaction = new Reaction { Content = content };
        Validator.ValidateObject(reaction, new ValidationContext(reaction), true);
        return await _feedbackRepository.CreateReactionToPostById(postId, reaction, account);
    }
}