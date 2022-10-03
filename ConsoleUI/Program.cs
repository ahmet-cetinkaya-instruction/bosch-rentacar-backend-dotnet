//// IoC
//IBrandDal brandDal = new InMemoryBrandDal();
//BrandBusinessRules brandBusinessRules = new(brandDal);
////

//IBrandService brandService = new BrandManager(brandDal, brandBusinessRules);

////
//CreateBrandRequest brandToAdd = new() { Name = "Toyota" };
//CreateBrandRequest brandToAdd2 = new() { Name = "BMW" };
////CreateBrandRequest brandToException = new() { Id = 3, Name = "BMW" };

//brandService.Add(brandToAdd);
//brandService.Add(brandToAdd2);
////brandService.Add(brandToException);
////

////
//GetBrandResponse brandToUpdate = brandService.GetById(1);
//UpdateBrandRequest updateBrandRequest = new() { Id = brandToUpdate.Id, Name = "Audi" };
//brandService.Update(updateBrandRequest);
////

////
//DeleteBrandRequest deleteBrandRequest = new() { Id = 2 };
//brandService.Delete(deleteBrandRequest);
////

////
//List<ListBrandResponse> brandList = brandService.GetList();
//foreach (ListBrandResponse brand in brandList)
//    Console.WriteLine($"Id : {brand.Name}");
////