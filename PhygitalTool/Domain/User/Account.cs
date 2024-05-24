using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Phygital.Domain.Feedback;

namespace Phygital.Domain.User;

public class Account : IdentityUser
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name is too short, min 3 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Lastname is required.")]
    [MinLength(3, ErrorMessage = "Lastname is too short, min 3 characters.")]
    public string LastName { get; set; }
    public Organisation Organisation { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Like> Likes { get; set; }
}