using System.Collections;
using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Feedback;
using Phygital.Domain.Questionsprocess;
using Phygital.Domain.Questionsprocess.Questions;

namespace Phygital.Domain.Themas;

public class Theme : IValidatableObject
{
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public ICollection<Theme> SubThemas { get; set; }

    public ICollection<Flow> Flows { get; set; }
    public ICollection<FlowElement> FlowElements { get; set; }
    
    public ICollection<Post> Posts { get; set; }

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