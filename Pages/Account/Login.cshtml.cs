using System.Security.Claims;
using AuthRazor.Data.Dtos;
using AuthRazor.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthRazor.Pages.Account;

public class Login : PageModel
{
    private readonly IAccountService _accountService;

    public Login(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty] public LoginDto LoginDto { get; set; } = new();

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var res = await _accountService.Login(LoginDto);
            if (res.Data)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LoginDto.UserName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
                };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);

                return RedirectToPage("/Index");
            }

            return Page();
        }
        catch (Exception e)
        {
            return RedirectToPage("/Error");
        }
    }
}