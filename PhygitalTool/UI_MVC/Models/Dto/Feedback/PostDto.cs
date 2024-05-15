using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class PostDto : IValidatableObject
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title is too long, max 255 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Text is required.")]
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Text { get; set; }
    
    public long ThemeId { get; set; }
    
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInText = Text.Split(' ');
        foreach (var word in wordsInText)
        {
            if (vulgarWords.Contains(word))
            {
                string errorMessage = "Geen vulgaire taal in text!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Text)}));
            }
        }
        
        var wordsInTitle = Title.Split(' ');
        foreach (var word in wordsInTitle)
        {
            if (vulgarWords.Contains(word))
            {
                string errorMessage = "Geen vulgaire taal in title!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Title)}));
            }
        }
        return errors;
    }
}