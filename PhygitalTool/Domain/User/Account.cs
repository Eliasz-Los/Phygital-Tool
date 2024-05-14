using Microsoft.AspNetCore.Identity;

namespace Phygital.Domain.User;

public class Account : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
   //Email hoeft nie omdat die al in IdentityUser zit
    // public string Mail { get; set; }
}