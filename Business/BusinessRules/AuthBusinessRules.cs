using Business.Requests.Auth;
using Core.Business.Exceptions;
using Core.CrossCuttingConcerns.Security.Entities;
using Core.CrossCuttingConcerns.Security.Hashing;

namespace Business.BusinessRules;

public class AuthBusinessRules
{
    public void CheckIfPasswordIsEqualConfirmation(string password, string passwordConfirmation)
    {
        if (password != passwordConfirmation)
            throw new BusinessException("Password don't match.");
    }
    public void CheckIfPasswordMatch(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        bool isPasswordMatch =
            HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        if (!isPasswordMatch)
            throw new BusinessException("Users password don't match");
    }
}