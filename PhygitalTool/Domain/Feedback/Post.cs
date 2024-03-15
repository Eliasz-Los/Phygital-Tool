using Phygital.Domain.Themas;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Post
    // TODO Image moet nog toegevoegd worden maar weet niet hoe
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Like> Likes { get; set; }
    public Theme Theme { get; set; }
    
    // Link to the user who made the post
    public Account Account { get; set; }
    
}