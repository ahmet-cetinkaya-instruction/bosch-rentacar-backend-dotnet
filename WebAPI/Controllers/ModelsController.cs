using Business.Abstracts;
using Business.Requests.Models;
using Business.Responses.Models;
using Core.Business.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("{Id}")]
        public GetModelResponse GetById([FromRoute] GetModelRequest request)
        {
           return _modelService.GetById(request);
        }

        [HttpGet]
        public PaginateListModelResponse GetList([FromQuery] PageRequest request)
        {
            return _modelService.GetList(request);
        }
    }
}
