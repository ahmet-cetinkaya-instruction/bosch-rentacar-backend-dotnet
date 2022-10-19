using Core.CrossCuttingConcerns.Security.Entities;

namespace Core.CrossCuttingConcerns.Security.Token;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, ICollection<OperationClaim> operationClaims);
}