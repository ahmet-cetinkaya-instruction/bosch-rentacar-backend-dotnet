using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfModelDal:EfEntityRepositoryBase<Model, RentACarBoschContext>, IModelDal
    {
    }
}
