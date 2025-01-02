using IfoodParaguai.Models;
using IfoodParaguai.PassHash;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IfoodParaguai.Services;

public class UserService
{
    private readonly IMongoCollection<Usuario> _userCollection;

    public UserService(
        IOptions<UserDataSettings> userStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userStoreDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<Usuario>(
            userStoreDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<Usuario>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

    public async Task<Usuario?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();

    public async Task Create(Usuario newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(ObjectId id, Usuario updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id.ToString() == id);

    public async Task CreateAsync(RequisicaoUsuario user)
    {
        CriptografarSenha hasher = new CriptografarSenha();
        string salt = hasher.GerarSalt();
        string hash = hasher.GerarHash(user.password, salt);

        Usuario usuario = new Usuario()
        {
            Id = ObjectId.GenerateNewId(),  
            nome = user.nome,
            email = user.email,
            idade = user.idade,
            hash = hash,
            salt = salt
        };

        await _userCollection.InsertOneAsync(usuario);
    }
    public async Task<Usuario> Verify(string email, string password)
    {
        List<Usuario> usuarios = await GetAsync();
        Usuario? usuario = usuarios.FirstOrDefault(u => u.email == email);

        if (usuario == null)
        {
            return null;
        }
        else
        {
            CriptografarSenha hasher = new CriptografarSenha();
            bool response = hasher.VerificarSenha(password, usuario.salt, usuario.hash);

            return response ? usuario : null;
        }


    }
}
