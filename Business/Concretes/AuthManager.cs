using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Requests.Auth;
using Business.Requests.Users;
using Business.Responses.Auth;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;
using Core.CrossCuttingConcerns.Security.Hashing;
using Core.CrossCuttingConcerns.Security.Token;

namespace Business.Concretes;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, IMapper mapper, AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _mapper = mapper;
        _authBusinessRules = authBusinessRules;
        _tokenHelper = tokenHelper;
    }

    public AccessResponse Register(RegisterUserRequest request)
    {
        // todo: check if users email exists
        _authBusinessRules.CheckIfPasswordIsEqualConfirmation(request.Password, request.PasswordConfirmation);

        CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(request);
        byte[] passwordSalt,
               passwordHash;
        HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
        createUserRequest.PasswordHash = passwordHash;
        createUserRequest.PasswordSalt = passwordSalt;
        _userService.Add(createUserRequest);

        GetUserResponse getUserResponse = _userService.GetByMail(request.Email);
        User user = _mapper.Map<User>(getUserResponse);
        AccessToken accessToken = createAccessToken(user);

        AccessResponse response = new() { AccessToken = accessToken };
        return response;
    }

    public AccessResponse Login(LoginUserRequest request)
    {
        GetUserResponse user = _userService.GetByMail(request.Email);
        //todo: check password Verification
        //todo: create access token
        AccessResponse response = new() { AccessToken = new AccessToken() };
        return response;
    }

    private AccessToken createAccessToken(User user)
    {
        GetUsersClaimsResponse getUsersClaimsResponse = _userService.GetClaims(new GetUsersClaimsRequest { Id = user.Id });
        AccessToken accessToken = _tokenHelper.CreateToken(user, getUsersClaimsResponse.Claims);
        return accessToken;
    }
}