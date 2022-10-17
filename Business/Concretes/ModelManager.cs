using AutoMapper;
using Business.Abstracts;
using Business.Requests.Models;
using Business.Responses.Models;
using Core.Business.Mailing;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes;

public class ModelManager : IModelService
{
    private IModelDal _modelDal;
    private IMapper _mapper;

    public ModelManager(IModelDal modelDal, IMapper mapper)
    {
        _modelDal = modelDal;
        _mapper = mapper;
    }

    public GetModelResponse GetById(GetModelRequest request)
    {
        Model? model = _modelDal.Get(predicate: m => m.Id == request.Id,
                                     include: m => m.Include(mi => mi.Brand));

        GetModelResponse response = _mapper.Map<GetModelResponse>(model);
        return response;
    }
}