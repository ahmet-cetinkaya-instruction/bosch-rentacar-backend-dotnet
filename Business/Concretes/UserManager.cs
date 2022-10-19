using AutoMapper;
using Business.Abstracts;
using Business.Requests.Users;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class UserManager : IUserService
{
    private IUserDal _userDal;
    private IMapper _mapper;

    public UserManager(IUserDal userDal, IMapper mapper)
    {
        _userDal = userDal;
        _mapper = mapper;
    }

    public GetUsersClaimsResponse GetClaims(GetUsersClaimsRequest request)
    {
        User? user = _userDal.Get(u => u.Id == request.Id);
        ICollection<OperationClaim> claims = _userDal.GetClaims(user);
        GetUsersClaimsResponse? response = _mapper.Map<GetUsersClaimsResponse>(claims);
        return response;
    }

    public GetUserResponse GetByMail(string email)
    {
        User? user = _userDal.Get(u => u.Email == email);
        GetUserResponse? response = _mapper.Map<GetUserResponse>(user);
        return response;
    }

    public void Add(CreateUserRequest request)
    {
        User user = _mapper.Map<User>(request);
        _userDal.Add(user);
    }
}