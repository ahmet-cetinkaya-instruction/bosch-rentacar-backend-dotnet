using Core.CrossCuttingConcerns.Security.Entities;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;

namespace DataAccess.Concretes.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, RentACarBoschContext>, IUserDal
{
    // include'a alternif bir sorgu işlemi
    public ICollection<OperationClaim> GetClaims(User user) // user: id 1
    {
        using (RentACarBoschContext context = new())
        {
            // Linq Queries
            IQueryable<OperationClaim> query = from operationClaim in context.OperationClaims
                                               join userOperationClaim in context.UserOperationClaims // inner join
                                                   on operationClaim.Id equals userOperationClaim.OperationClaimId
                                               // id 1, name admin, userId 1, id 1, operationId 1
                                               // id 2, name brands.add, userId 2, id 2, operationId 2
                                               // id 3, name brand.update, userId 1, id 3, operationId 3
                                               where userOperationClaim.Id == user.Id
                                               // id 1, name admin, userId 1, id 1, operationId 1
                                               // id 3, name brand.update, userId 1, id 3, operationId 3
                                               select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                                                // id 1, name admin
                                                // id 3, name brand.update

                                                /* SQL Karşılığı:
                                                SELECT o.Id, o.Name FROM dbo.OperationClaims o
                                                    JOIN dbo.UserOperationClaims uo
                                                    ON o.Id = uo.OperationClaimId
                                                    WHERE uo.Id = 1;
                                                */
            return query.ToHashSet();
        }
    }
}