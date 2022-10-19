using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.CrossCuttingConcerns.Security.Entities;
using Core.CrossCuttingConcerns.Security.Token.Encryption;
using Core.CrossCuttingConcerns.Security.Token.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.CrossCuttingConcerns.Security.Token.JWT;

public class JwtHelper: ITokenHelper
{
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, ICollection<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        // jwt'deki signature kısmı için hangi security key ve algoritma oluşturacağımızı belirliyoruz.
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

        // JwtSecurityToken'a ait bilgileri oluşturduk.
        JwtSecurityToken jwt = createJwtSecurityToken(user, operationClaims, signingCredentials);
        // JwtSecurityTokenHandler ile jwt string'imizi oluşturduk.
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, ExpirationTime = _accessTokenExpiration };
    }

    private JwtSecurityToken createJwtSecurityToken(
        User user, ICollection<OperationClaim> operationClaims, SigningCredentials signingCredentials)
    {
        return new JwtSecurityToken(issuer: _tokenOptions.Issuer,
                                    audience: _tokenOptions.Audience,
                                    expires: _accessTokenExpiration,
                                    notBefore: DateTime.Now,
                                    claims: setClaims(user, operationClaims),
                                    signingCredentials: signingCredentials
        );
    }

    private IEnumerable<Claim> setClaims(User user, ICollection<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(o=>o.Name).ToArray());
        return claims;
    }
}