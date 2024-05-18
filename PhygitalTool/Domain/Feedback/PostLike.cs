namespace Phygital.Domain.Feedback;

public class PostLike
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsLiked { get; set; } = false;
    public Post Post { get; set; }
    public Like Like { get; set; }
}