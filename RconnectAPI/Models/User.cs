using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RconnectAPI.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public User(string username, byte[] password, string email, string firstname, string lastname, DateTime birthdate, List<string>? hobbies = null, List<string>? contacts = null, List<string>? rating = null, List<string>? missedmeetings = null)
        {
            Username = username;
            Password = password;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Birthdate = birthdate;
            Hobbies = hobbies;
            Contacts = contacts;
            Rating = rating;
            Missedmeetings = missedmeetings;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>();
        public List<string> Contacts { get; set; } = new List<string>();
        public List<string> Rating { get; set; } = new List<string>();
        public List<string> Missedmeetings { get; set; } = new List<string>();



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
