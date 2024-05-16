using System.Net;
using AuthRazor.Data.Dtos;
using AuthRazor.Response;
using Microsoft.AspNetCore.Identity;

namespace AuthRazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<string>> Register(RegisterDto model)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(model.UserName);
                if (result != null)
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "Such a user already exists!");
                }

                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (createUserResult.Succeeded)
                {
                    return new Response<string>("success");
                }
                else
                {
                    return new Response<string>(HttpStatusCode.BadRequest, "Invalid");
                }
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> Login(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return new Response<bool>(HttpStatusCode.BadRequest, "User not found");
                }

                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, model.Password);

                return new Response<bool>(checkPasswordResult) ;
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

       
    }
}
