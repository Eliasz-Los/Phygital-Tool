namespace Phygital.Domain.Feedback;

public class PostLike
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow.ToUniversalTime().AddHours(2);
    public Post Post { get; set; }
    public Like Like { get; set; }
}