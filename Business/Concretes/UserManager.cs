using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Requests.Users;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IMapper _mapper;


    public UserManager(IUserDal userDal, IMapper mapper, UserBusinessRules userBusinessRules)
    {
        _userDal = userDal;
        _mapper = mapper;
        _userBusinessRules = userBusinessRules;
    }

    public GetUsersClaimsResponse GetClaims(GetUsersClaimsRequest request)
    {
        User? user = _userDal.Get(u => u.Id == request.Id);
        //todo: Check if user exists
        ICollection<OperationClaim> claims = _userDal.GetClaims(user);
        GetUsersClaimsResponse? response = _mapper.Map<GetUsersClaimsResponse>(claims);
        return response;
    }

    public GetUserResponse GetByMail(string email)
    {
        User user = GetUserByMail(email);
        GetUserResponse response = _mapper.Map<GetUserResponse>(user);
        return response;
    }

    public User GetUserByMail(string email)
    {
        User? user = _userDal.Get(u => u.Email == email);
        _userBusinessRules.CheckIfUserExists(user);
        return user;
    }

    public void Add(CreateUserRequest request)
    {
        User user = _mapper.Map<User>(request);
        _userDal.Add(user);
    }
}