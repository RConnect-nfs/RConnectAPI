using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RconnectAPI.Models
{
    public class Host
    {
        public Host(string? id, string name, string description, string adress, string city, string phone, List<string> openinghours)
        {
            Id = id;
            Name = name;
            Description = description;
            Adress = adress;
            City = city;
            Phone = phone;
            Openinghours = openinghours;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public List<string> Openinghours { get; set; } = new List<string>();
    }
}
