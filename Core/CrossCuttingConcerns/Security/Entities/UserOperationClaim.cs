using Core.Entities;

namespace Core.CrossCuttingConcerns.Security.Entities;

// User One-Many Operation Claim
public class UserOperationClaim : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
// 1, 1, 1
// 2, 1, 4