using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.User;

public class Account : IdentityUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string mail { get; set; }
    public string Password { get; set; }    
    public string RoleName { get; set; }
}