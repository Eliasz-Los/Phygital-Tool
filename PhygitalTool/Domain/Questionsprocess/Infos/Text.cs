using System.ComponentModel.DataAnnotations;

namespace Phygital.Domain.Questionsprocess.Infos;

public class Text : Info
{
    
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    [MaxLength(500, ErrorMessage = "Title is too long, max 500 characters.")]
    public override string Title { get; set; }
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
}