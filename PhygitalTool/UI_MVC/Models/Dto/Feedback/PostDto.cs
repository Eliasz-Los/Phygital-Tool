using System.ComponentModel.DataAnnotations;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class PostDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title is too long, max 255 characters.")]
    public string Title { get; set; }
    [MaxLength(1000, ErrorMessage = "Text is too long, max 1000 characters.")]
    public string Text { get; set; }
}