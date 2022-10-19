using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Requests.Auth;
using Business.Requests.Users;
using Business.Responses.Auth;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;

namespace Business.Concretes;

public class AuthManager : IAuthService
{
    private IUserService _userService;
    private IMapper _mapper;
    private AuthBusinessRules _authBusinessRules;

    public AuthManager(IUserService userService, IMapper mapper, AuthBusinessRules authBusinessRules)
    {
        _userService = userService;
        _mapper = mapper;
        _authBusinessRules = authBusinessRules;
    }

    public AccessResponse Register(RegisterUserRequest request)
    {
        // todo: check if users email exists
        _authBusinessRules.CheckIfPasswordIsEqualConfirmation(request.Password, request.PasswordConfirmation);
        
        CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(request);
        //todo: add password hashing
        _userService.Add(createUserRequest);
        //todo: create access token
        AccessResponse response = new(){AccessToken = new()};
        return response;
    }

    public AccessResponse Login(LoginUserRequest request)
    {
        GetUserResponse user = _userService.GetByMail(request.Email);
        //todo: check password Verification
        //todo: create access token
        AccessResponse response = new(){AccessToken = new AccessToken()};
        return response;
    }
}