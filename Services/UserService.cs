using IfoodParaguai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

    public async Task<List<Usuario>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<Usuario?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Usuario newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, Usuario updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);

    public async Task CreateAsync(Usuario newUser)
    {
        await _userCollection.InsertOneAsync(newUser);
    }
}
