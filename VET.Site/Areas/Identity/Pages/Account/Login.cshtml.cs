// <copyright file="Login.cshtml.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using VET.DataBase.Identity;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
      //  private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager //,
           // IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
           // _emailSender = emailSender;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Campo requerido.")]
            [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Campo requerido.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Recuérdame?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl =  Url.Content("~/Home/Index");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return Page();
                }
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("El usuario ha iniciado sesión.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Cuenta de usuario bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        //public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = await _userManager.FindByEmailAsync(Input.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        //    }

        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var callbackUrl = Url.Page(
        //        "/Account/ConfirmEmail",
        //        pageHandler: null,
        //        values: new { userId = userId, code = code },
        //        protocol: Request.Scheme);
        //    await _emailSender.SendEmailAsync(
        //        Input.Email,
        //        "Confirm your email",
        //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //    ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        //    return Page();
        //}
    }
}
