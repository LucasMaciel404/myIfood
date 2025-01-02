using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace IfoodParaguai.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string? nome { get; set; }
    public int idade { get; set; }
    public string email { get; set; }
    public string hash { get; set; }
    public string salt { get; set; }
    public Usuario()
    {
        Id = ObjectId.GenerateNewId();
    }
}
public class RequisicaoUsuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? nome { get; set; }
    public int idade { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}

public class RetornoUsuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string? nome { get; set; }
    public int idade { get; set; }
    public string email { get; set; }
}
public class UpdateUsuarioModel
{
    public string? nome { get; set; }
    public int idade { get; set; }
    public string email { get; set; }
    public string hash { get; set; }
    public string salt { get; set; }
}