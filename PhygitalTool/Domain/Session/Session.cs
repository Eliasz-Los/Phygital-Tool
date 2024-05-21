using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Datatypes;

namespace Phygital.Domain.Session;

public class Session : IValidatableObject
{
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SessionType SessionType { get; set; }
    public Installation Installation { get; set; }
    public ICollection<Participation> Participations { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (StartDate > DateTime.Now)
        {
            results.Add(new ValidationResult("Start date cannot be in the future.", new[] { nameof(StartDate) }));
        }

        if (EndDate < StartDate)
        {
            results.Add(new ValidationResult("End date cannot be before start date.", new[] { nameof(EndDate) }));
        }

        if (EndDate == StartDate)
        {
            results.Add(new ValidationResult("End date cannot be the same as start date.", new[] { nameof(EndDate) }));
        }

        return results;
    }
}