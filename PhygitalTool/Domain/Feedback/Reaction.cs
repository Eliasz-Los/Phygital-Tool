using System.ComponentModel.DataAnnotations;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Reaction
{
    public long Id { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<PostReaction> PostReactions { get; set; }
    
    // Link to the user who posted the reaction
    //public Account Account { get; set; }
    
}