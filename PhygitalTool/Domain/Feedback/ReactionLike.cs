namespace Phygital.Domain.Feedback;

public class ReactionLike
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow.ToUniversalTime().AddHours(2);
    public Reaction Reaction { get; set; }
    public Like Like { get; set; }
}