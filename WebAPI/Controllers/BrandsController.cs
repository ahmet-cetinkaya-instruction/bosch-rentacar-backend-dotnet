using Business.Abstracts;
using Business.BusinessRules;
using Business.Contretes;
using Business.Responses.Brands;
using DataAccess.Abstracts;
using DataAccess.Concretes.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route(template: "api/[controller]")] // Attribute
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController() //todo: IoC - Inversion of Control
        {
            IBrandDal brandDal = new InMemoryBrandDal();
            BrandBusinessRules brandBusinessRules = new(brandDal);
            _brandService = new BrandManager(brandDal, brandBusinessRules);
        }

        [HttpGet]
        public List<ListBrandResponse> GetList()
        {
            List<ListBrandResponse> result = _brandService.GetList();
            return result;
        }
    }
}
