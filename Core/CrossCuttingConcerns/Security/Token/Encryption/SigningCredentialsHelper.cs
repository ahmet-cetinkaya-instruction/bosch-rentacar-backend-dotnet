using Microsoft.IdentityModel.Tokens;

namespace Core.CrossCuttingConcerns.Security.Token.Encryption;

public static class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
    }
}