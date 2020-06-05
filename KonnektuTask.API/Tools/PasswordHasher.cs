using System;
using System.Linq;
using System.Security.Cryptography;

namespace KonnektuTask.API.Tools
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int saltSize = 16;
        private const int keySize = 32;
        private const int iterations = 10000;

        public string HashPassword(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return $"{iterations}.{salt}.{key}";
        }

        public bool VerifyPasswordHash(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3) throw new FormatException("Unexpected hash format");

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(keySize);

            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}