using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace IfoodParaguai.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? nome { get; set; }
    public int idade { get; set; }
    public string email { get; set; }
    public Usuario()
    {
        Id = null;
    }
}
