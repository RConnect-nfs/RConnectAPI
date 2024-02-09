using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RconnectAPI.Models
{
    public class Meeting
    {
        public Meeting(List<string> users, string host, DateTime date, List<string> billedusers)
        { 
            Users = users;
            Host = host;
            Date = date;
            Billedusers = billedusers;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public List<string> Users { get; set; } = new List<string>();
        public string Host { get; set; }
        public DateTime Date { get; set; }
        public List<string> Billedusers { get; set; } = new List<string>();
    }
}
