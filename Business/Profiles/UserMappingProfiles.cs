using AutoMapper;
using Business.Requests.Users;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;

namespace Business.Profiles;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        CreateMap<ICollection<OperationClaim>, GetUsersClaimsResponse>();
        CreateMap<User, GetUserResponse>();
        CreateMap<CreateUserRequest, User>();
    }
}