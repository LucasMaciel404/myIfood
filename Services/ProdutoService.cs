using IfoodParaguai.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IfoodParaguai.Services;

public class ProdutoService
{
    private readonly IMongoCollection<Produto> _produtosCollection;

    public ProdutoService(IOptions<ProdutoDataSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _produtosCollection = database.GetCollection<Produto>(settings.Value.ProdutoCollectionName);
    }

    public async Task<List<Produto>> GetAsync() =>
        await _produtosCollection.Find(x => true).ToListAsync();

    public async Task<Produto> GetAsync(string id) =>
        await _produtosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Produto newProduto) =>
        await _produtosCollection.InsertOneAsync(newProduto);

    public async Task UpdateAsync(string id, Produto produto) =>
        await _produtosCollection.ReplaceOneAsync(x => x.Id == id, produto);

    public async Task DeleteAsync(string id) =>
        await _produtosCollection.DeleteOneAsync(x => x.Id == id);
}