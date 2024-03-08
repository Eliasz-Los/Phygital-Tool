using System.ComponentModel.DataAnnotations;

namespace Domain.Feedback;

public class Post
    // TODO Image moet nog toegevoegd worden maar weet niet hoe
{
    public long postId { get; set; }
    public string title { get; set; }
    public string text { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Like> Likes { get; set; }
    
    // Link to the user who made the post
    public long userId { get; set; }
    
}