using Core.Entities;

namespace Core.CrossCuttingConcerns.Security.Entities;

// Authentication: Kimlik Doğrulama
public class User : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    // kullanıcı 1: Passw0rd$DIOAJSKJMN#>£#£$
    // kullanıcı 2: Passw0rd$ZLXKLŞZKLŞK#$>$
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
}
