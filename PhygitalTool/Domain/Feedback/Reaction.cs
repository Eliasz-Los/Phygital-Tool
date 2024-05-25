using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Reaction : IValidatableObject
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
    public ICollection<PostReaction> PostReactions { get; set; }
    public Account Account { get; set; }
    
    public ICollection<ReactionLike> ReactionLikes { get; set; }
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInContent = Content.Split(' ');
        var regex = new Regex("[^a-zA-Z]");
        foreach (var word in wordsInContent)
        {
            var cleanedWord = regex.Replace(word, "").ToLower();
            if (vulgarWords.Contains(cleanedWord))
            {
                string errorMessage = "Geen vulgaire taal in text!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Content)}));
            }
        }
        
        return errors;
    }
}