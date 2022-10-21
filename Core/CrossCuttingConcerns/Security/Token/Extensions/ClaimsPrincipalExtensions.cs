using System.Security.Claims;

namespace Core.CrossCuttingConcerns.Security.Token.Extensions;

public static class ClaimsPrincipalExtensions
{
    // ClaimsPrincipal bir kullanıcının getirdiği token ile, o anki clamlere ulaşmak için bir .net classı
    public static ICollection<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        HashSet<string> result = claimsPrincipal.FindAll(claimType).Select(c => c.Value).ToHashSet();
        return result;
    }
    public static ICollection<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims(ClaimTypes.Role);
    }
}