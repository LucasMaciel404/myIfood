using Microsoft.AspNetCore.Mvc;
using IfoodParaguai.Models;
using IfoodParaguai.Services;

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
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var pedido = await _pedidoService.GetByIdAsync(id);
            if (pedido == null)
                return NotFound(new { Message = "Pedido não encontrado." });

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            await _pedidoService.CreateAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pedido updatedPedido)
        {
            var updated = await _pedidoService.UpdateAsync(id, updatedPedido);
            if (!updated)
                return NotFound(new { Message = "Pedido não encontrado." });

            return NoContent();
        }

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
