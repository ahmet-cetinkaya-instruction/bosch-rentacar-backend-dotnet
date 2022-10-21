using System.Security.Authentication;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using Core.Business.Exceptions;
using Core.CrossCuttingConcerns.Security.Exceptions;
using Core.CrossCuttingConcerns.Security.Token.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac;

public class SecuredOperation:MethodInterception
{
    private string[] _requiredClaims;
    private IHttpContextAccessor _httpContextAccessor;

    public SecuredOperation(string claims)
    {
        _requiredClaims = claims.Split(',');
        //_httpContextAccessor = httpContextAccessor;
        // controller > business > dal. Dependency zincirinde aspectler bulunmuyor.
        // IoC'inin kurulu olduğu web api buradaki constructor'ı göremeyecektir.
        // Dolayısıyla constructor'dan IoC işlemi başarısız olacaktır.
        // Bunun yerine Dependency yakalayan ve bizim injection alt yapımızı okuyan, bilen bir araca ihtiyacımız var.
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var isAuthenticate = _httpContextAccessor.HttpContext.User.Claims.IsNullOrEmpty();
        if (isAuthenticate)
            throw new AuthenticationException();

        ICollection<string> usersRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        bool isAuthorize = _requiredClaims.All(requiredClaim => usersRoleClaims.Contains(requiredClaim));
        if(!isAuthorize) 
                throw new AuthorizeException("You are not authorize.");
        //foreach (var requiredClaim in _requiredClaims)
        //{
        //    if (!usersRoleClaims.Contains(requiredClaim))
        //        throw new Exception("You are not authorize.");
        //}
    }
}