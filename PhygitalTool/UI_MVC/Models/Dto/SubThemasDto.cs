using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.UI_MVC.Models.Dto;

public class SubThemasDto : IValidatableObject
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Description { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>(); 
        if (Title.Length < 3)
        {
            results.Add(
                new ValidationResult("Title should be at least 3 characters long !",
                    new string[] { "Title" }));
        }

        return results;
    }
}



