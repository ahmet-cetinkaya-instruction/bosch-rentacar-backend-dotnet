using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.InMemory
{
    public class InMemoryBrandDal : IBrandDal
    {
        private List<Brand> _brands;

        public InMemoryBrandDal()
        {
            _brands = new List<Brand>();
        }

        public void Add(Brand brand)
        {
            _brands.Add(brand);
        }

        public void Delete(Brand brand)
        {
           _brands.Remove(brand);
        }

        public Brand? GetById(int id)
        {
            return _brands.FirstOrDefault(b => b.Id == id);
        }

        public Brand? GetByName(string name)
        {
            return _brands.FirstOrDefault(b => b.Name == name);
        }

        public List<Brand> GetList()
        {
            return _brands;
        }

        public void Update(Brand brand)
        {
            var brandToUpdate = _brands.First(b=>b.Id == brand.Id);
            brandToUpdate.Name = brand.Name;
        }
    }
}
