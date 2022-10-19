using Business.Abstracts;
using Business.Requests.Auth;
using Business.Responses.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("register")]
        public AccessResponse Register(RegisterUserRequest request)
        {
            AccessResponse result = _authService.Register(request);
            return result;
        }
    }
}
