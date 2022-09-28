using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IBrandDal
    {
        Brand? GetById(int id);
        Brand? GetByName(string name);
        List<Brand> GetList();
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);
    }
}
