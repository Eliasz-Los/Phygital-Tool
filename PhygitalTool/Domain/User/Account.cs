using Microsoft.AspNetCore.Identity;

namespace Phygital.Domain.User;

public class Account //: IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
    public string RoleName { get; set; }
}