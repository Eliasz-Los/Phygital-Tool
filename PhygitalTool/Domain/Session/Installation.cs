using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Session;

public class Installation
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name is too short, min 3 characters.")]
    [MaxLength(255, ErrorMessage = "Name is too long, max 255 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "PostalCode is required.")]
    [Range(1000, 9999, ErrorMessage = "Postal code should be between 1000 and 9999.")]
    public int PostalCode { get; set; }
    [Required(ErrorMessage = "Street is required.")]
    [MinLength(3, ErrorMessage = "Streetname is too short, min 3 characters.")]
    [MaxLength(255, ErrorMessage = "Title is too long, max 255 characters.")]
    public string Street { get; set; }
    [Required(ErrorMessage = "StreetNumber is required.")]
    [Range(1, 9999, ErrorMessage = "Street number should be between 1 and 9999.")]
    public int StreetNumber { get; set; }
    public ICollection<Session> Sessions { get; set; }
}