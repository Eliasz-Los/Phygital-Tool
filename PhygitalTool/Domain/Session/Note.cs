using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Session;

public class Note
{
    public long Id { get; set; }
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Comment { get; set; }
    public Participation Participation { get; set; }
}