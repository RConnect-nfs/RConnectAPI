using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RconnectAPI.Models;
public class Utilisateur
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("NomUtilisateur")]
    public string NomUtilisateur { get; set; }

    [BsonElement("MotDePasse")]
    public string MotDePasse { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }
}
