using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.User;

public class Organisation
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Name is too short, min 3 characters.")]
    public string Name { get; set; }
    [Required]
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Description { get; set; }
    public ICollection<Account> Accounts { get; set; }
}