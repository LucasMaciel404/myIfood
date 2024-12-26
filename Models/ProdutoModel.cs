using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace IfoodParaguai.Models;

public class Produto 
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? id { get; set; }
    public string? nome { get; set; }
    public decimal preco { get; set; }
    public Produto()
    {
        id = null;
    }
}