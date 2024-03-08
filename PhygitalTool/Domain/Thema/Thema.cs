using System.ComponentModel.DataAnnotations;

namespace Domain.Thema;

public class Thema : IValidatableObject
{
    public long Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Thema> SubThemas { get; set; }

    public Thema(string title, string description, ICollection<Thema> subThemas)
    {
        Title = title;
        Description = description;
        SubThemas = subThemas;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>(); 
        if (this.Title.Length < 3)
        {
            results.Add(
                new ValidationResult("Title should be at least 3 characters long !",
                    new string[] { "Title" }));
        }

        return results;
    }
}