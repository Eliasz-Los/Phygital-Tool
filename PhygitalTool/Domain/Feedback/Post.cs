using Phygital.Domain.Themas;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public ICollection<PostReaction> PostReactions { get; set; }
    public ICollection<PostLike> PostLikes { get; set; }
    public Theme Theme { get; set; }
    
    // Link to the user who made the post
    //public Account Account { get; set; }
    
}