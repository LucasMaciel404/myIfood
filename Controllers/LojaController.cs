using IfoodParaguai.Models;
using IfoodParaguai.Services;
using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Controllers
{
    [ApiController]
    [Route("/v1/[controller]/")]
    public class LojaController : Controller
    {
        private readonly LojaService _lojaService;

        public LojaController(LojaService lojaService) =>
          _lojaService = lojaService;

        [HttpGet]
        public async Task<List<Loja>> Get() =>
          await _lojaService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Loja>> Get(string id)
        {
            var loja = await _lojaService.GetAsync(id);

            if (loja is null)
            {
                return NotFound();
            }

            return loja;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Loja newLoja)
        {
            await _lojaService.Create(newLoja);

            return CreatedAtAction(nameof(Get), new { id = newLoja.Id }, newLoja);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Loja updatedLoja)
        {
            var loja = await _lojaService.GetAsync(id);

            if (loja is null)
            {
                return NotFound();
            }

            updatedLoja.Id = loja.Id;

            await _lojaService.UpdateAsync(id, updatedLoja);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var loja = await _lojaService.GetAsync(id);

            if (loja is null)
            {
                return NotFound();
            }

            await _lojaService.RemoveAsync(id);

            return NoContent();
        }
    }
}