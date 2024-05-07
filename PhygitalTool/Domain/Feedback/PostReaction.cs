namespace Phygital.Domain.Feedback;

public class PostReaction
{
    public long Id { get; set; }
    public Post Post { get; set; }
    public Reaction Reaction { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}