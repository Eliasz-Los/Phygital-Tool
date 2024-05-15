using System.ComponentModel.DataAnnotations;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class ReactionDto : IValidatableObject
{
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
    
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInContent = Content.Split(' ');
        foreach (var word in wordsInContent)
        {
            if (vulgarWords.Contains(word))
            {
                string errorMessage = "Geen vulgaire taal in text!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Content)}));
            }
        }
        
        return errors;
    }
}