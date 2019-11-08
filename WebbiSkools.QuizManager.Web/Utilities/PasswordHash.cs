using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace WebbiSkools.QuizManager.Web.Utilities
{
    public class PasswordHash
    {
        public static string Create(string password, string username)
        {
            byte[] salt = Encoding.UTF8.GetBytes(username);

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }
    }
}
