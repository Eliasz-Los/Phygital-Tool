namespace Domain.Feedback;

public class Reaction
// TODO link leggen met de gebruiker die de reactie plaatst
{
    public long Id { get; set; }
    public string content { get; set; }
    public ICollection<Like> Likes { get; set; }

    public Reaction(string content, ICollection<Like> likes)
    {
        this.content = content;
        Likes = likes;
    }
}