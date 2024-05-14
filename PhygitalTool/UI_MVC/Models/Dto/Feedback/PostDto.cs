using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class PostDto : IValidatableObject
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title is too long, max 255 characters.")]
    public string Title { get; set; }
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Text { get; set; }
    
    public long ThemeId { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInText = Text.Split(' ');
        foreach (var word in wordsInText)
        {
            if (vulgarWords.Contains(word))
            {
                yield return new ValidationResult("Vulgar words are not allowed in the text.", new[] { nameof(Text) });
            }
        }
    }
}