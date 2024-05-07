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
        _feedbackRepository.CreatePost(post);
    }

    public async Task<PostLike> AddPostLikeByPostId(long postId)
    {
        return await _feedbackRepository.LikePost(postId);
    }

    public async Task<PostLike> RemovePostLikeByPostId(long postId, long likeId)
    {
        return await _feedbackRepository.DeletePostLike(postId, likeId);
    }
}