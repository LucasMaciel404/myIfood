using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace IfoodParaguai.Models;

public class Loja
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public Loja()
    {
        Id = null;
    }
}