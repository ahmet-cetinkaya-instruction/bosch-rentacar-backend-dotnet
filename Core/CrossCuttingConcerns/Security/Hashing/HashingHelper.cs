using System.Security.Cryptography;
using System.Text;

namespace Core.CrossCuttingConcerns.Security.Hashing;

public static class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) //ödev: out, ref araştırınız.
    {
        using (HMACSHA512 hmac = new())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new(passwordSalt))
        {
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            // 0-255
            //int i = 0;
            for (int i = 0; i < computedHash.Length; ++i)
            {
                //if i < computedHash.Length

                //Func<int, int> iPlusPlus = i =>
                //               {
                //                   int temp = i;
                //                   i = i + 1;
                //                   return temp;
                //               },
                //               plusPlusI = i =>
                //               {
                //                   i = i + 1;
                //                   return i;
                //               };
                //var result = iPlusPlus(i); // i++; // 0
                //// 1
                //var result2 = plusPlusI(i); // ++i; // 1
                //// 1
                //++i;

                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }
    }

    }
