using System.ComponentModel.DataAnnotations;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class ReactionDto : IValidatableObject
{
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
    
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