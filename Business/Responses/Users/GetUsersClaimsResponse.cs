using Core.CrossCuttingConcerns.Security.Entities;

namespace Business.Responses.Users;

public class GetUsersClaimsResponse
{
    public ICollection<OperationClaim> Claims { get; set; }
}