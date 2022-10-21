using Core.Business.Exceptions;
using Core.CrossCuttingConcerns.Security.Entities;

namespace Business.BusinessRules;

public class UserBusinessRules
{
    public void CheckIfUserExists(User user)
    {
        if (user is null)
            throw new BusinessException("User dont exists");
    }
}