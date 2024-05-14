using Microsoft.AspNetCore.Identity;

namespace Phygital.Domain.User;

public class Account : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public Organisation Organisation { get; set; }
}