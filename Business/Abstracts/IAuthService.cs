using Business.Requests.Auth;
using Business.Responses.Auth;

namespace Business.Abstracts;

public interface IAuthService
{
    AccessResponse Register(RegisterUserRequest request);
    AccessResponse Login(LoginUserRequest request);
}