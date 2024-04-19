using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Phygital.Domain.User;

namespace Phygital.UI_MVC.Services;

//TODO not implemented 
public class CustomSignInManager : SignInManager<Account>
{
    public CustomSignInManager(UserManager<Account> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Account> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<Account>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<Account> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }
    
    public new async Task<SignInResult> PasswordSignInAsync(string userNameOrEmail, string password, bool isPersistent,
        bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userNameOrEmail) ?? await  UserManager.FindByEmailAsync(userNameOrEmail);
        if (user == null)
        {
            return  SignInResult.Failed;
        }

        return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }
}