using Business.Abstracts;
using Business.BusinessRules;
using Business.Requests.Brands;
using Business.Responses.Brands;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Contretes
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        BrandBusinessRules _brandBusinessRules;

        public BrandManager(IBrandDal brandDal, BrandBusinessRules brandBusinessRules)
        {
            _brandDal = brandDal; // 100
            _brandBusinessRules = brandBusinessRules; // 101
        }

        public void Add(CreateBrandRequest request)
        {
            _brandBusinessRules.CheckIfBrandNameNotExists(request.Name);

            Brand brandToAdd = new Brand { Name = request.Name };

            _brandDal.Add(brandToAdd);
        }

        public void Delete(DeleteBrandRequest request)
        {
            _brandBusinessRules.CheckIfBrandExists(request.Id);

            Brand brandToDelete = new Brand { Id = request.Id };

            _brandDal.Delete(brandToDelete);
        }

        public List<ListBrandResponse> GetList()
        {
            List<Brand> brands = _brandDal.GetList();

            List<ListBrandResponse> response = new();
            foreach (Brand brand in brands)
                response.Add(new ListBrandResponse { Id = brand.Id, Name = brand.Name });

            return response;
        }

        public GetBrandResponse GetById(int id)
        {
            Brand? brand = _brandDal.GetById(id);
            _brandBusinessRules.CheckIfBrandExists(brand);

            GetBrandResponse response = new() { Id = brand!.Id, Name = brand.Name };
            return response;
        }

        public void Update(UpdateBrandRequest request)
        {
            Brand? brandToUpdate = _brandDal.GetById(request.Id);
            _brandBusinessRules.CheckIfBrandExists(brandToUpdate);

            brandToUpdate!.Name = request.Name;
            _brandDal.Update(brandToUpdate);
        }
    }
}
