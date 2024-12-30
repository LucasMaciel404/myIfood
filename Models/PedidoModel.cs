using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IfoodParaguai.Models;

public class Pedido
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? id { get; set; }
    public string? id_simples { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? loja_id { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? cliente_id { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId? produto_id { get; set; }
    public bool? em_transito { get; set; }
    public bool? status { get; set; }
    public string[]? loja { get; set; }
    public string[]? cliente { get; set; }
    public string[]? produto { get; set; }
    public Pedido()
    {
        Random numAleatorio = new Random();
        int valorInteiro = numAleatorio.Next(1, 100);
        int segundos = DateTime.Now.Second;
        int media = (valorInteiro * segundos) / 2;
        id_simples = $"{media}";
        loja = null;
        cliente = null;
        produto = null;
    }
}
public class PedidoRequisicao
{
    public string? id_simples { get; set; }
    public string? loja_id { get; set; }
    public string? cliente_id { get; set; }
    public string? produto_id { get; set; }
    public bool? em_transito { get; set; }
    public bool? status { get; set; }
    public PedidoRequisicao()
    {
        Random numAleatorio = new Random();
        int valorInteiro = numAleatorio.Next(1, 100);
        int segundos = DateTime.Now.Second;
        int media = (valorInteiro * segundos) / 2;
        id_simples = $"{media}";
    }
}