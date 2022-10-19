using Business.Abstracts;

namespace Business.Responses.Auth;

public class AccessResponse
{
    public AccessToken AccessToken { get; set; } // JWT, JSON WEB TOKEN
}