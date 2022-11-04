using AutoMapper;
using Business.Responses.Models;
using Core.DataAccess.Paging;
using Entities.Concretes;

namespace Business.Profiles
{
    public class ModelMappingProfiles : Profile
    {
        public ModelMappingProfiles()
        {
            CreateMap<Model, GetModelResponse>();
            CreateMap<Model, ListModelResponse>()
                .ForMember(m=>m.BrandName, opt=>opt.MapFrom(m=>m.Brand.Name));
            CreateMap<IPaginate<Model>, PaginateListModelResponse>();
        }
    }
}
