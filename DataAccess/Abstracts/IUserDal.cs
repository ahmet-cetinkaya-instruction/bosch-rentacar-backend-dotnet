using Core.CrossCuttingConcerns.Security.Entities;
using Core.DataAccess;

namespace DataAccess.Abstracts;
public interface IUserDal : IEntityRepository<User>
{
    ICollection<OperationClaim> GetClaims(User user);
}
