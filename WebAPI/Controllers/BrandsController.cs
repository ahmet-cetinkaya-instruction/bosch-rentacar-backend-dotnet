using Business.Abstracts;
using Business.Requests.Brands;
using Business.Responses.Brands;
using Microsoft.AspNetCore.Mvc;

// GetList: GET api/brands
// GetById: GET api/brands/1
// Add: POST api/brands
// Update: PUT api/brands/1
// Delete: DELETE api/brands/1

namespace WebAPI.Controllers
{
    [Route(template: "api/[controller]")] // Attribute
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService; // 102
        }

        [HttpGet]
        public List<ListBrandResponse> GetList()
        {
            List<ListBrandResponse> result = _brandService.GetList();
            return result;
        }

        [HttpGet(template: "{id}")]
        public GetBrandResponse GetById(int id)
        {
            GetBrandResponse result = _brandService.GetById(id);
            return result;
        }

        [HttpPost]
        public void Add(CreateBrandRequest request)
        {
            _brandService.Add(request);
        }

        [HttpPut]
        public void Update(UpdateBrandRequest request)
        {
            _brandService.Update(request);
        }

        [HttpDelete(template: "{Id}")]
        public void Delete([FromRoute] DeleteBrandRequest request)
        {
            _brandService.Delete(request);
        }
    }
}
