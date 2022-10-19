using AutoMapper;
using Business.Requests.Auth;
using Business.Requests.Users;

namespace Business.Profiles;

public class AuthMappingProfiles: Profile
{
    public AuthMappingProfiles()
    {
        CreateMap<RegisterUserRequest, CreateUserRequest>();
    } 
}