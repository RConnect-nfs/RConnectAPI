﻿namespace RconnectAPI.Models
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System;
    using System.Security.Cryptography;

    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive the hashed password
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Return the salt and the hashed password for storage
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public static bool VerifyPassword(string hashedPasswordWithSalt, string password)
        {
            var parts = hashedPasswordWithSalt.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var hashedPassword = parts[1];

            // Hash the incoming password using the same salt and compare
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword == hashed;
        }
    }
}