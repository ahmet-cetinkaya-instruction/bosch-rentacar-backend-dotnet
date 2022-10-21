using Business.Requests.Users;
using Business.Responses.Users;
using Core.CrossCuttingConcerns.Security.Entities;

namespace Business.Abstracts;

public interface IUserService
{
    GetUsersClaimsResponse GetClaims(GetUsersClaimsRequest request);
    GetUserResponse GetByMail(string email);
    User GetUserByMail(string email);
    void Add(CreateUserRequest request);
}