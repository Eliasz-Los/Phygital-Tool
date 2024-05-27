using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Infos;

public class Video : Info
{
    // public Flow Flow { get; set; }
    // public Theme SubTheme { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Url is required.")]
    public string Url { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    [MaxLength(1000, ErrorMessage = "Description is too long, max 1000 characters.")]
    public string Description { get; set; }
}