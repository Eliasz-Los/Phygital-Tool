namespace Phygital.Domain.Feedback;

public class Reaction
{
    public long reactionId { get; set; }
    public string content { get; set; }
    public ICollection<Like> Likes { get; set; }
    
    // Link to the user who posted the reaction
    public long userId { get; set; }
    
    // Link to the post where the reaction belongs to
    public long postId { get; set; }


    public Reaction(string content, ICollection<Like> likes, long userId, long postId)
    {
        this.content = content;
        Likes = likes;
        this.userId = userId;
        this.postId = postId;
    }
}