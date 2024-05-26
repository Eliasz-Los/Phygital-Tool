using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phygital.UI_MVC.Models.Dto.Infos;

public class ImageDto
{
    public long flowId { get; set; }
    public long subthemeId { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    public string Title { get; set; }
    
    [DataType(DataType.ImageUrl)]
    public string Url { get; set; }
    
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    
    [Required(ErrorMessage = "AltText is required.")]
    [MaxLength(1000, ErrorMessage = "AltText is too long, max 1000 characters.")]
    public string AltText { get; set; }
}