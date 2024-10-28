using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common
{
    public class PasswordHasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            // Generate a random salt
            byte[] saltBytes = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            // Compute the hash using the salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = new SHA256Managed().ComputeHash(passwordBytes);
            string hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Compute the hash using the stored salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + storedSalt);
            byte[] hashBytes = new SHA256Managed().ComputeHash(passwordBytes);
            string hash = Convert.ToBase64String(hashBytes);

            // Compare the computed hash with the stored hash
            return hash == storedHash;
        }
    }
}
