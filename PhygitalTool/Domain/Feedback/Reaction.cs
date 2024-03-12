using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Reaction
{
    public long Id { get; set; }
    public string Content { get; set; }
    public ICollection<Like> Likes { get; set; }
    
    // Link to the user who posted the reaction
    public Account Account { get; set; }
    
    // Link to the post where the reaction belongs to
    public Post Post { get; set; }
}