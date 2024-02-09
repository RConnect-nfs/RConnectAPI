using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RconnectAPI.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public User(string? id, byte[] password, string firstname, string lastname, string username, string email, string name, DateTime dob)
        {
            Id = id;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Name = name;
            Dob = dob;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public byte[] Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>();
        public List<string> Contacts { get; set; } = new List<string>();

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

                var iterations = 10000; 
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
