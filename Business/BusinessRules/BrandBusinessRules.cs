using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class BrandBusinessRules
    {
        IBrandDal _brandDal;

        public BrandBusinessRules(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void CheckIfBrandExists(Brand? brand)
        {
            if (brand == null)
                throw new Exception("Brand not be exists.");
        }

        public void CheckIfBrandNotExists(Brand? brand)
        {
            if (brand != null)
                throw new Exception("Brand already exists.");
        }

        public void CheckIfBrandExists(int brandId)
        {
            Brand? brand = _brandDal.GetById(brandId);
            CheckIfBrandExists(brand);
        }

        public void CheckIfBrandNameNotExists(string brandName)
        {
            Brand? brand = _brandDal.GetByName(brandName);
            CheckIfBrandNotExists(brand);
        }
    }
}
