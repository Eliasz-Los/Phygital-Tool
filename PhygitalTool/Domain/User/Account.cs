using Microsoft.AspNetCore.Identity;
using Phygital.Domain.Feedback;

namespace Phygital.Domain.User;

public class Account : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public Organisation Organisation { get; set; }
    
    public ICollection<Post> Posts { get; set; }
    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Like> Likes { get; set; }
}