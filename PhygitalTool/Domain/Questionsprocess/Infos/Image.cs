using System.ComponentModel.DataAnnotations;
using Phygital.Domain.Themas;

namespace Phygital.Domain.Questionsprocess.Infos;

public class Image : Info
{
    public long Id { get; set; }
    public Flow Flow { get; set; }
    public Theme SubTheme { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Url is required.")]
    public string Url { get; set; }
    [Required(ErrorMessage = "AltText is required.")]
    [MaxLength(1000, ErrorMessage = "AltText is too long, max 1000 characters.")]
    public string AltText { get; set; }
}