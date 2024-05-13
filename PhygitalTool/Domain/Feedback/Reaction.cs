using System.ComponentModel.DataAnnotations;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Reaction : IValidatableObject
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<PostReaction> PostReactions { get; set; }
    
    // Link to the user who posted the reaction
    //public Account Account { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInText = Content.Split(' ');
        foreach (var word in wordsInText)
        {
            if (vulgarWords.Contains(word))
            {
                yield return new ValidationResult("Vulgar words are not allowed in the text.", new[] { nameof(Content) });
            }
        }
    }
}