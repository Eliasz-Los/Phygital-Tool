using Phygital.Domain.Datatypes;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Like
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow.ToUniversalTime().AddHours(2);
    public LikeType LikeType { get; set; }
    public ICollection<PostLike> PostLikes { get; set; }
    public Account Account { get; set; }
    public ICollection<ReactionLike> ReactionLikes { get; set; }
}