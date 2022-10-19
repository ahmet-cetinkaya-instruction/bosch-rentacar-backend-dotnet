using Core.CrossCuttingConcerns.Security.Token;

namespace Business.Responses.Auth;

public class AccessResponse
{
    public AccessToken AccessToken { get; set; } // JWT, JSON WEB TOKEN
}