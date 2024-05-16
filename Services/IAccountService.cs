using AuthRazor.Data.Dtos;
using AuthRazor.Response;

namespace AuthRazor.Services;

public interface IAccountService
{
  Task<Response<string>> Register(RegisterDto register);
  Task<Response<bool>> Login(LoginDto login);
}