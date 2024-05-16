using AuthRazor.Data.Dtos;
using AuthRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthRazor.Pages.Account;

public class Register : PageModel
{
    private readonly IAccountService _accountService;

    public Register(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [BindProperty] public RegisterDto RegisterDto { get; set; } = new();
    
    public async Task<IActionResult> OnPost()
    {
        try
        {
            var res = await _accountService.Register(RegisterDto);
            if(res.StatusCode==200) return RedirectToPage("/Account/Login");
            return Page();
        }
        catch (Exception e)
        {
            return RedirectToPage("/Error");
        }
    }
}