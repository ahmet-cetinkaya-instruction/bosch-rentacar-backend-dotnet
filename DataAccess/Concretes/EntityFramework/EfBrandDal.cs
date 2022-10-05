using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBrandDal : IBrandDal
    {
        public Brand? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Brand? GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Brand> GetList()
        {
            throw new NotImplementedException();
        }

        public void Add(Brand brand)
        {
            // Stack (Heap'teki referans adresi) = Heap Değer Alanı
            using (RentACarBoschContext context = new())
            {
                context.Brands.Add(brand);

                context.SaveChanges(); // Unit of work
            }
        } // Garbage Collector

        public void Update(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void Delete(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
