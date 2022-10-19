using Core.Business.Exceptions;

namespace Business.BusinessRules;

public class AuthBusinessRules
{
    public void CheckIfPasswordIsEqualConfirmation(string password, string passwordConfirmation)
    {
        if (password != passwordConfirmation)
            throw new BusinessException("Password don't match.");
    }
}