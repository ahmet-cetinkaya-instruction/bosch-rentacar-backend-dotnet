using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.CrossCuttingConcerns.Security.Token.Encryption;

public static class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        // Simetrik anahtar kriptografi'de ilgili mesajların şifrelenmesi ve şifresinin çözülmesinde kullanılan anahtardır, şifreleme düzenidir.
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}