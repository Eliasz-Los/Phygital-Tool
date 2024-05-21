namespace Phygital.Domain.Feedback;

public class PostReaction
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow.ToUniversalTime().AddHours(2);
    public Post Post { get; set; }
    public Reaction Reaction { get; set; }
}