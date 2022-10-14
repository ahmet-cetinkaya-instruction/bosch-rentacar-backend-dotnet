using AutoMapper;
using Business.Responses.Models;
using Entities.Concretes;

namespace Business.Profiles
{
    public class ModelMappingProfiles : Profile
    {
        public ModelMappingProfiles()
        {
            CreateMap<Model, GetModelResponse>();
        }
    }
}
