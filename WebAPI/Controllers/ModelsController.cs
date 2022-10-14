using Business.Abstracts;
using Business.Requests.Models;
using Business.Responses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
