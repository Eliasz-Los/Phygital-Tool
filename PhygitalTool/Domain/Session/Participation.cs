using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Questionsprocess;

namespace Phygital.Domain.Session;

public class Participation : IValidatableObject
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration
    {
        get
        {
            var duration = EndTime - StartTime;
            return $"{duration.Hours}h {duration.Minutes}m {Math.Ceiling((double)duration.Seconds)}s";
        }
    }
    public int AmountOfParticipants { get; set; }
    public Session Session { get; set; }
    public Flow Flow { get; set; }
    public ICollection<Note> Notes { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (StartTime > DateTime.Now)
        {
            results.Add(new ValidationResult("Start date cannot be in the future.", new[] { nameof(StartTime) }));
        }

        if (EndTime < StartTime)
        {
            results.Add(new ValidationResult("End date cannot be before start date.", new[] { nameof(EndTime) }));
        }

        if (EndTime == StartTime)
        {
            results.Add(new ValidationResult("End date cannot be the same as start date.", new[] { nameof(EndTime) }));
        }

        return results;
    }
}