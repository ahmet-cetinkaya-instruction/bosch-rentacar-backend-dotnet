using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Requests.Brands;
using Business.Responses.Brands;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes;

public class BrandManager : IBrandService
{
    private readonly IBrandDal _brandDal;
    private readonly BrandBusinessRules _brandBusinessRules;
    private IMapper _mapper;

    public BrandManager(IBrandDal brandDal, BrandBusinessRules brandBusinessRules, IMapper mapper)
    {
        _brandDal = brandDal; // 100
        _brandBusinessRules = brandBusinessRules; // 101
        _mapper = mapper;
    }

    public void Add(CreateBrandRequest request)
    {
        //_brandBusinessRules.CheckIfBrandNameNotExists(request.Name);

        Brand brandToAdd = _mapper.Map<Brand>(request); // AutoMapper Reflection

        _brandDal.Add(brandToAdd);
    }

    public void Delete(DeleteBrandRequest request)
    {
        _brandBusinessRules.CheckIfBrandExists(request.Id);

        Brand brandToDelete = _mapper.Map<Brand>(request);

        _brandDal.Delete(brandToDelete);
    }

    public List<ListBrandResponse> GetList()
    {
        List<Brand> brands = _brandDal.GetList();

        List<ListBrandResponse> response = _mapper.Map<List<ListBrandResponse>>(brands);

        return response;
    }

    public GetBrandResponse GetById(int id)
    {
        Brand? brand = _brandDal.GetById(id);
        _brandBusinessRules.CheckIfBrandExists(brand);

        GetBrandResponse response = _mapper.Map<GetBrandResponse>(brand);

        return response;
    }

    public void Update(UpdateBrandRequest request)
    {
        Brand? brand = _brandDal.GetById(request.Id);
        _brandBusinessRules.CheckIfBrandExists(brand);

        Brand brandToUpdate = _mapper.Map<Brand>(request);

        _brandDal.Update(brandToUpdate);
    }
}