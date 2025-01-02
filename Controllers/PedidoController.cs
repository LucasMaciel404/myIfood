using Microsoft.AspNetCore.Mvc;
using IfoodParaguai.Models;
using IfoodParaguai.Services;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace IfoodParaguai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // Métodos CRUD
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            var algunsPedidos = pedidos.Select(p => new
            {
                id = p.id_simples,
                loja = p.loja,
                cliente = p.cliente,
                produto = p.produto,
                status = p.status,
                em_transito = p.em_transito
            });
            return Ok(algunsPedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { Message = "Pedido não encontrado." });

            return Ok(pedido);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PedidoRequisicao pedido)
        {
            try
            {
                await _pedidoService.CreateAsync(pedido);
            }
            catch ( Exception e)
            {
                Console.WriteLine($"[ERROR]: {e.Message}");
            }
            return Ok(pedido);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pedido updatedPedido)
        {
            var updated = await _pedidoService.UpdateAsync(id, updatedPedido);
            if (!updated)
                return NotFound(new { Message = "Pedido não encontrado." });

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _pedidoService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Pedido não encontrado." });

            return NoContent();
        }
    }
}
