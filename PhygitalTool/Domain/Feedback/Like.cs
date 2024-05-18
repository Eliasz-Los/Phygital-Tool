using Phygital.Domain.Datatypes;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Like
{
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public LikeType LikeType { get; set; }
    
    public ICollection<PostLike> PostLikes { get; set; }
    public Reaction Reaction { get; set; }
    public Account Account { get; set; }
    
   
    
}