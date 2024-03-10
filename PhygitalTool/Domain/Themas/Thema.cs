using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Questionsprocess;

namespace Domain.Themas;

public class Thema : IValidatableObject
{
    public long Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Thema> SubThemas { get; set; }

    public ICollection<Flow> Flows { get; set; }
    public ICollection<FlowElement> FlowElements { get; set; }
    

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