using System.ComponentModel.DataAnnotations;

namespace Domain.Feedback;

public class Post
    // TODO Image moet nog toegevoegd worden maar weet niet hoe
    // TODO Link leggen met de gebruiker die de reactie heeft geplaatst
{
    public long Id { get; set; }
    public string title { get; set; }
    public string text { get; set; }
    public ICollection<Reaction> Reacties { get; set; }
    public ICollection<Like> Likes { get; set; }

    public Post(string title, string text, ICollection<Reaction> reacties, ICollection<Like> likes)
    {
        this.title = title;
        this.text = text;
        Reacties = reacties;
        Likes = likes;
    }
}