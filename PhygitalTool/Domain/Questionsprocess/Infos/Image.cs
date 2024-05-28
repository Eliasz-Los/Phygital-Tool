using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Phygital.Domain.Questionsprocess.Infos;

public class Image : Info
{ 
   
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(3, ErrorMessage = "Title is too short, min 3 characters.")]
    public override string Title { get; set; }
    [Required(ErrorMessage = "Url is required.")]
    [DataType(DataType.ImageUrl)]
    public string Url { get; set; }
    
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
    
    [Required(ErrorMessage = "AltText is required.")]
    [MaxLength(1000, ErrorMessage = "AltText is too long, max 1000 characters.")]
    public string AltText { get; set; }
}