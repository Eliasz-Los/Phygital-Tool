using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;
using Phygital.Domain.User;

namespace Phygital.Domain.Feedback;

public class Post : IValidatableObject
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title is too long, max 255 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Text is required.")]
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Text { get; set; }
    public DateTime PostTime { get; set; } = DateTime.UtcNow.ToUniversalTime().AddHours(2);
    public ICollection<PostReaction> PostReactions { get; set; }
    public ICollection<PostLike> PostLikes { get; set; }
    public Theme Theme { get; set; }
    public Account Account { get; set; }
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        var vulgarWords = File.ReadAllLines("vulgairewoorden.txt").ToList();
        var wordsInText = Text.Split(' ');
        foreach (var word in wordsInText)
        {
            if (vulgarWords.Contains(word.ToLower()))
            {
                string errorMessage = "Geen vulgaire taal in text!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Text)}));
            }
        }
        
        var wordsInTitle = Title.Split(' ');
        foreach (var word in wordsInTitle)
        {
            if (vulgarWords.Contains(word.ToLower()))
            {
                string errorMessage = "Geen vulgaire taal in title!!!";
                errors.Add(new ValidationResult(errorMessage, new []{nameof(Title)}));
            }
        }
        return errors;
    }
}