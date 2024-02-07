using System.Security.Cryptography;
using System.Text;

namespace RconnectAPI.Models
{
    public class User
    {

        public string Alias { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<int> Hobbies { get; set; } = new List<int>();
        public List<int> Contacts { get; set; } = new List<int>();

        public User(string alias, string password, string email, string name, int age, List<int> hobbies, List<int> contacts)
        {
            Alias = alias;
            Password = HashPassword(password);
            Email = email;
            Name = name;
            Age = age;
            Hobbies = hobbies;
            Contacts = contacts;
        }

        private byte[] HashPassword(string password)
        {
            // Hachage
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var iterations = 10000; // Ajuster le nombre d'itérations selon le besoin
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var key = new byte[hmac.HashSize / 8];
                for (int i = 0; i < iterations; i++)
                {
                    hmac.Initialize();
                    hmac.Key = hash;
                    hash = hmac.ComputeHash(salt);
                    Buffer.BlockCopy(hash, 0, key, 0, key.Length);
                }

                // Concatenate
                return salt.Concat(key).ToArray();
            }
        }
    }
}
