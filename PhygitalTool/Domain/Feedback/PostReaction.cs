namespace Phygital.Domain.Feedback;

public class PostReaction
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    
    public Post Post { get; set; }
    public Reaction Reaction { get; set; }
}