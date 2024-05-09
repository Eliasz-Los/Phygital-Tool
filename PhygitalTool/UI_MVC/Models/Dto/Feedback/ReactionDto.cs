using System.ComponentModel.DataAnnotations;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class ReactionDto
{
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
}