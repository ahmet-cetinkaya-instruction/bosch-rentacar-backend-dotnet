using Business.Abstracts;
using Business.BusinessRules;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contretes
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        BrandBusinessRules _brandBusinessRules;

        public BrandManager(IBrandDal brandDal, BrandBusinessRules brandBusinessRules)
        {
            _brandDal = brandDal;
            _brandBusinessRules = brandBusinessRules;
        }

        public void Add(Brand brand)
        {
            _brandBusinessRules.CheckIfBrandNameNotExists(brand.Name);

            _brandDal.Add(brand);
        }

        public void Delete(Brand brand)
        {
            _brandBusinessRules.CheckIfBrandExists(brand.Id);
            _brandDal.Delete(brand);
        }

        public List<Brand> GetList()
        {
            return _brandDal.GetList();
        }

        public Brand GetById(int id)
        {
            Brand? brand = _brandDal.GetById(id);
            _brandBusinessRules.CheckIfBrandExists(brand);

            return brand!;
        }

        public void Update(Brand brand)
        {
            Brand? brandToUpdate = _brandDal.GetById(brand.Id);
            _brandBusinessRules.CheckIfBrandExists(brand);

            brandToUpdate!.Name = brand.Name;
            _brandDal.Update(brandToUpdate);
        }
    }
}
