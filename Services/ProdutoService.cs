using IfoodParaguai.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IfoodParaguai.Services;

public class ProdutoService
{
    private readonly IMongoCollection<Produto> _produtos;

    public ProdutoService(IOptions<ProdutoDataSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _produtos = database.GetCollection<Produto>(settings.Value.ProdutoCollectionName);
    }

    public async Task<List<Produto>> GetAsync() =>
        await _produtos.Find(x => true).ToListAsync();

    public async Task<Produto> GetAsync(string id) =>
        await _produtos.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Produto produto) =>
        await _produtos.InsertOneAsync(produto);

    public async Task UpdateAsync(string id, Produto produto) =>
        await _produtos.ReplaceOneAsync(x => x.id == id, produto);

    public async Task DeleteAsync(string id) =>
        await _produtos.DeleteOneAsync(x => x.id == id);
}