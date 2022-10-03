using AutoMapper;
using Business.Requests.Brands;
using Business.Responses.Brands;
using Entities.Concretes;

namespace Business.Profiles
{
    public class BrandMappingProfiles : Profile
    {
        public BrandMappingProfiles()
        {
            CreateMap<CreateBrandRequest, Brand>();
            CreateMap<UpdateBrandRequest, Brand>();
            CreateMap<DeleteBrandRequest, Brand>();
            CreateMap<Brand, ListBrandResponse>(); // List<Brand> şeklinde belirtmeye gerek olmadan, Listeler arası map'leme işlemi gerçekleşiyor.
            CreateMap<Brand, GetBrandResponse>();
        }
    }
}
