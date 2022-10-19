namespace Core.CrossCuttingConcerns.Security.Token;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
}