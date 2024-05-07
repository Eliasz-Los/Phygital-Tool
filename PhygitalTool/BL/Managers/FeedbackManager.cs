using System.ComponentModel.DataAnnotations;
using Phygital.DAL;
using Phygital.Domain.Feedback;

namespace Phygital.BL.Managers;

public class FeedbackManager : IFeedbackManager
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackManager(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Post> GetPostByIdAsync(long postId)
    {
        return await _feedbackRepository.ReadPostByIdAsync(postId);
    }

    public async Task<IEnumerable<Post>> GetAllPostsWithReactionsAndLikes()
    {
        return await _feedbackRepository.ReadAllPostsWithReactionsAndLikes();
    }

    public void AddPost(string title, string text)
    {
        var post = new Post
        {
            Title = title,
            Text = text
        };
        Validator.ValidateObject(post, new ValidationContext(post), true);
        _feedbackRepository.CreatePost(post);
    }

    public async Task UpdatePost(long postId, string title, string text)
    {
        var post = await _feedbackRepository.ReadPostByIdAsync(postId);
        if (post == null)
        {
            throw new Exception($"Post with id {postId} not found");
        }
        post.Title = title;
        post.Text = text;
        Validator.ValidateObject(post, new ValidationContext(post), true);
        _feedbackRepository.UpdatePost(post);
    }

    public void DeletePost(long postId)
    {
     
        _feedbackRepository.DeletePost(postId);
    }

    public async Task<PostLike> AddPostLikeByPostId(long postId)
    {
        return await _feedbackRepository.LikePost(postId);
    }

    public async Task<PostLike> RemovePostLikeByPostId(long postId, long likeId)
    {
        return await _feedbackRepository.DeletePostLike(postId, likeId);
    }

    public Task<Reaction> AddReactionToPostById(long postId, string content)
    {
        var reaction = new Reaction { Content = content };
        Validator.ValidateObject(reaction, new ValidationContext(reaction), true);
        return _feedbackRepository.CreateReactionToPostById(postId, reaction);
    }
}