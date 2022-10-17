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
    private IMailService _mailService;

    public ModelManager(IModelDal modelDal, IMapper mapper, IMailService mailService)
    {
        _modelDal = modelDal;
        _mapper = mapper;
        _mailService = mailService;
    }

    public GetModelResponse GetById(GetModelRequest request)
    {
        Model? model = _modelDal.Get(predicate: m => m.Id == request.Id,
                                     include: m => m.Include(mi => mi.Brand));

        GetModelResponse response = _mapper.Map<GetModelResponse>(model);

        _mailService.SendMail(new Mail()
        {
            TextBody = "",
            HtmlBody = "",
            Subject = "Model Get",
            ToFullName = "Ahmet Çetinkaya",
            ToMail = "ahmetcetinkaya7@outlook.com"
        });

        return response;
    }
}