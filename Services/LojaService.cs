using IfoodParaguai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IfoodParaguai.Services;

public class LojaService 
{
    private readonly IMongoCollection<Loja> _lojaCollection;

    public LojaService(
        IOptions<LojaDataSettings> lojaStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
          lojaStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
          lojaStoreDatabaseSettings.Value.DatabaseName);

        _lojaCollection = mongoDatabase.GetCollection<Loja>(
          lojaStoreDatabaseSettings.Value.LojaCollectionName);
    }

    public async Task<List<Loja>> GetAsync() =>
      await _lojaCollection.Find(_ => true).ToListAsync();

    public async Task<Loja?> GetAsync(string id) =>
      await _lojaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Loja newLoja) =>
      await _lojaCollection.InsertOneAsync(newLoja);

    public async Task UpdateAsync(string id, Loja updatedLoja) =>
      await _lojaCollection.ReplaceOneAsync(x => x.Id == id, updatedLoja);

    public async Task RemoveAsync(string id) =>
      await _lojaCollection.DeleteOneAsync(x => x.Id == id);
}