using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
public class PedidoRetorno
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? id { get; set; }
    public ObjectId? loja_id { get; set; }
    public ObjectId? cliente_id { get; set; }
    public ObjectId? produto_id { get; set; }
    public bool? em_transito { get; set; }
    public bool? status { get; set; }
    public object[] loja { get; set; }
    public object[] produto { get; set; }
    public object[] cliente { get; set; }
}
public class PedidoRetornoClean
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? id { get; set; }
    public object[] cliente { get; set; }
    public object[] produto { get; set; }
    public object[] loja { get; set; }
    public bool? em_transito { get; set; }
    public bool? status { get; set; }
}