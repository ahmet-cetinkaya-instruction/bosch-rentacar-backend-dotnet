using Business.Abstracts;
using Business.BusinessRules;
using Business.Contretes;
using DataAccess.Abstracts;
using DataAccess.Concretes.InMemory;
using Entities.Concretes;

// IoC
IBrandDal brandDal = new InMemoryBrandDal();
BrandBusinessRules brandBusinessRules = new(brandDal);
//

IBrandService brandService = new BrandManager(brandDal, brandBusinessRules);

//
Brand brandToAdd = new Brand { Id = 1, Name = "Toyota" };
Brand brandToAdd2 = new Brand { Id = 2, Name = "BMW" };

//Brand brandToException = new Brand { Id = 3, Name = "BMW" };

brandService.Add(brandToAdd);
brandService.Add(brandToAdd2);
//brandService.Add(brandToException);
//

//
Brand brandToUpdate = brandService.GetById(1);
brandToUpdate.Name = "Audi";
brandService.Update(brandToUpdate);
//

//
Brand brandToDelete = brandService.GetById(2);
brandService.Delete(brandToDelete);
//

//
List<Brand> brandList = brandService.GetList();
foreach (Brand brand in brandList)
    Console.WriteLine(brand.Name);
//