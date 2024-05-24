using System.ComponentModel.DataAnnotations;
using Phygital.Domain.User;

namespace Phygital.UI_MVC.Models.Dto.Feedback;

public class ReactionReadDto
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(1000, ErrorMessage = "Content is too long, max 1000 characters.")]
    public string Content { get; set; }
    public string AccountName { get; set; }
    
    /*public int LikeCount { get; set; } 
    public int DislikeCount { get; set; } */
}