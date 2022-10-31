using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects.Autofac;
using Business.BusinessRules;
using Business.Requests.Brands;
using Business.Responses.Brands;
using Business.ValidationRules.FluentValidation.Brands;
using Core.Aspects;
using Core.Business.Mailing;
using Core.Business.Requests;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes;

public class BrandManager : IBrandService
{
    private readonly IBrandDal _brandDal;
    private readonly BrandBusinessRules _brandBusinessRules;
    private IMapper _mapper;
    private IMailService _mailService;
    public BrandManager(IBrandDal brandDal, BrandBusinessRules brandBusinessRules, IMapper mapper, IMailService mailService)
    {
        _brandDal = brandDal; // 100
        _brandBusinessRules = brandBusinessRules; // 101
        _mapper = mapper;
        _mailService = mailService;
    }

    [SecuredOperation("brands.add")]
    [ValidationAspect(typeof(CreateBrandRequestValidator))]
    [CacheRemoveAspect("IBrandService.Get")]
    [LogAspect(typeof(FileLogger))]
    //[LogAspect(typeof(MongoDbLogger))]
    public void Add(CreateBrandRequest request)
    {
        _brandBusinessRules.CheckIfBrandNameNotExists(request.Name);
        Brand brandToAdd = _mapper.Map<Brand>(request); // AutoMapper Reflection
        _brandDal.Add(brandToAdd);
    }

    [CacheRemoveAspect("IBrandService.Get")]
    [TransactionAspect]
    public void Delete(DeleteBrandRequest request)
    {
        _brandBusinessRules.CheckIfBrandExists(request.Id);

        Brand brandToDelete = _mapper.Map<Brand>(request);

        _brandDal.Delete(brandToDelete);

        // Diyelimki bu brand'a ait modeller de silinsin.
        // Bu durum da modellerin birinde ortaya çıkan hataya karşın transaction kullanabiliriz.
    }

    [PerformanceAspect(2)]
    [CacheAspect(10)]
    public PaginateListBrandResponse GetList(PageRequest request)
    {
        IPaginate<Brand> brands = _brandDal.GetList(index: request.Index, 
                                                    size: request.Size, 
                                                    orderBy: b => b.OrderBy(b => b.Name));

        PaginateListBrandResponse response = _mapper.Map<PaginateListBrandResponse>(brands);

        return response;
    }

    [CacheAspect(10)]
    public GetBrandResponse GetById(int id)
    {
        Brand? brand = _brandDal.Get(b => b.Id == id);
        _brandBusinessRules.CheckIfBrandExists(brand);

        GetBrandResponse response = _mapper.Map<GetBrandResponse>(brand);

        return response;
    }

    // AOP - Aspect Oriented Programming
    //[SecuredOperation("brands.update")]
    //[PerformanceTracking(2)]
    //[Transaction]
    //[Validation]
    //[Logging]
    // Interceptor
    [CacheRemoveAspect("IBrandService.Get")]
    public void Update(UpdateBrandRequest request)  // Reflection
    {
        Brand? brand = _brandDal.Get(b => b.Id == request.Id);
        _brandBusinessRules.CheckIfBrandExists(brand);

        Brand brandToUpdate = _mapper.Map<Brand>(request);

        _brandDal.Update(brandToUpdate);

        _mailService.SendMail(new Mail()
        {
            TextBody = "",
            HtmlBody = "",
            Subject = $"Brand({brandToUpdate.Id}) has updated.",
            ToFullName = "Ahmet Çetinkaya",
            ToMail = "ahmetcetinkaya7@outlook.com"
        });

    }
}