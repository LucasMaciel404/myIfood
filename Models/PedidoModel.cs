using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace IfoodParaguai.Models;

public class Pedido
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? id { get; set; }
    public string? loja_id { get; set; }
    public string? cliente_id { get; set; }
    public string? produto_id { get; set; }
    public bool? em_transito { get; set; }
    public bool? status { get; set; }
    public Pedido()
    {
        id = null;
    }
}