using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand,RentACarBoschContext>, IBrandDal
    {
        //public Brand? GetById(int id)
        //{
        //    using (RentACarBoschContext context = new())
        //    {
        //        return context.Brands.SingleOrDefault(b => b.Id == id);
        //    }
        //}

        //public Brand? GetByName(string name)
        //{
        //    using (RentACarBoschContext context = new())
        //    {
        //        return context.Brands.FirstOrDefault(b => b.Name == name);
        //    }
        //}

        //public List<Brand> GetList()
        //{
        //    using (RentACarBoschContext context = new())
        //    {
        //        return context.Brands.ToList(); // select * from brands
        //    }
        //}

        //public void Add(Brand brand)
        //{
        //    // Stack (Heap'teki referans adresi) = Heap Değer Alanı
        //    using (RentACarBoschContext context = new())
        //    {
        //        context.Brands.Add(brand);

        //        context.SaveChanges(); // Unit of work
        //    }
        //} // Garbage Collector

        //public void Update(Brand brand)
        //{
        //    using (RentACarBoschContext context = new())
        //    {
        //        var entity = context.Entry(brand); // Parametre olarak gelen nesne'nin referansını yakala
        //        entity.State = EntityState.Modified; // Entry'ın durumunu güncelliyoruz.
        //        context.SaveChanges();
        //    }
        //}

        //public void Delete(Brand brand)
        //{
        //    using (RentACarBoschContext context = new())
        //    {
        //        var entity = context.Entry(brand);
        //        entity.State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}
    }
}
