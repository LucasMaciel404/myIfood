using MongoDB.Driver;
using IfoodParaguai.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace IfoodParaguai.Services
{
    public class PedidoService
    {
        private readonly IMongoCollection<Pedido> _pedidoCollection;

        public PedidoService(IConfiguration configuration)
        {
            // Lendo os valores de configuração
            var connectionString = configuration["PedidoStoreDatabase:ConnectionString"];
            var databaseName = configuration["PedidoStoreDatabase:DatabaseName"];
            var collectionName = configuration["PedidoStoreDatabase:PedidoCollectionName"];

            // Criando a conexão com o MongoDB
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _pedidoCollection = database.GetCollection<Pedido>(collectionName);
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            // Define o estágio de lookup para a coleção "Lojas"
            var lookupLojas = new BsonDocument
            {
                { "$lookup", new BsonDocument
                    {
                        { "from", "Lojas" },
                        { "localField", "loja_id" },
                        { "foreignField", "_id" },
                        { "as", "loja" }
                    }
                }
            };

            // Define o estágio de lookup para a coleção "Produtos"
            var lookupProdutos = new BsonDocument
            {
                { "$lookup", new BsonDocument
                    {
                        { "from", "Produtos" },
                        { "localField", "produto_id" },
                        { "foreignField", "_id" },
                        { "as", "produto" }
                    }
                }
            };

            // Define o estágio de lookup para a coleção "Usuarios"
            var lookupUsuarios = new BsonDocument
            {
                { "$lookup", new BsonDocument
                    {
                        { "from", "Usuarios" },
                        { "localField", "cliente_id" },
                        { "foreignField", "_id" },
                        { "as", "cliente" }
                    }
                }
            };

            // Define a pipeline de agregação
            var pipeline = new List<BsonDocument> { lookupLojas, lookupProdutos, lookupUsuarios };

            // Executa a agregação
            var result = await _pedidoCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

            // Deserializa os resultados para o tipo Pedido
            var pedidos = result.Select(doc => BsonSerializer.Deserialize<Pedido>(doc)).ToList();

            return pedidos;
        }


        public async Task<Pedido?> GetByIdAsync(string id)
        {
            return await _pedidoCollection.Find(p => p.id_simples == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PedidoRequisicao pedido)
        {
            var montandoPedido = new Pedido
            {
                id = ObjectId.GenerateNewId(),
                produto_id = ObjectId.Parse(pedido.produto_id),
                cliente_id = ObjectId.Parse(pedido.cliente_id),
                loja_id = ObjectId.Parse(pedido.loja_id),
                em_transito = pedido.em_transito,
                status = pedido.status
            };
            await _pedidoCollection.InsertOneAsync(montandoPedido);
        }

        public async Task<bool> UpdateAsync(string id, Pedido updatedPedido)
        {
            var result = await _pedidoCollection.ReplaceOneAsync(p => p.id_simples == id, updatedPedido);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _pedidoCollection.DeleteOneAsync(p => p.id_simples == id);
            return result.DeletedCount > 0;
        }
    }
}
