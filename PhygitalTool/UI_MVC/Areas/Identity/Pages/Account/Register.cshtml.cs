using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Phygital.Domain.User;
using Phygital.BL;

namespace Phygital.UI_MVC.Areas.Identity.Pages.Account
{
    [Authorize]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Domain.User.Account> _signInManager;
        private readonly UserManager<Domain.User.Account> _userManager;
        private readonly IUserStore<Domain.User.Account> _userStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserManager _userManagerService;

        public RegisterModel(
            UserManager<Domain.User.Account> userManager,
            IUserStore<Domain.User.Account> userStore,
            SignInManager<Domain.User.Account> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserManager userManagerService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userManagerService = userManagerService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }
            
            [Display(Name = "Organisation")]
            public long? OrganisationId { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IList<Organisation> Organisations { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (User.IsInRole("Owner"))
            {
                Organisations = _userManagerService.GetAllOrganisations().ToList();
            }
            else if (User.IsInRole("Admin") || User.IsInRole("Subadmin"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser is { Organisation: not null })
                {
                    Input.OrganisationId = currentUser.Organisation.Id;
                }
                Console.WriteLine("onGetAsync User: {0}", currentUser?.Name);
                Console.WriteLine("onGetAsync Organisatie: {0}", currentUser?.Organisation);
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Domain.User.Account
                {
                    Name = Input.Name, 
                    LastName = Input.LastName, 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                };
                // Fetch the current user's details
                var currentUser = await _userManager.GetUserAsync(User);
                
                // If the current user has an organisation, set the new user's organisation to the same
                if (currentUser?.Organisation != null)
                {
                    _logger.LogError("Current user has an organisation, setting to current user's organisation");
                    user.Organisation = currentUser.Organisation;
                }
                else if (Input.OrganisationId.HasValue)
                {
                    _logger.LogError("Organisation found, setting to organisation");
                    user.Organisation = _userManagerService.GetOrganisationById(Input.OrganisationId.Value);
                }
                else
                {
                    _logger.LogError("No organisation found, setting to default organisation");
                    user.Organisation = _userManagerService.GetOrganisationById(1);
                }
                
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private Domain.User.Account CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Domain.User.Account>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Domain.User.Account)}'. " +
                    $"Ensure that '{nameof(Domain.User.Account)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Domain.User.Account> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Domain.User.Account>)_userStore;
        }
    }
}