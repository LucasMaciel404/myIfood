using IfoodParaguai.Models;
using IfoodParaguai.Services;
using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : Controller
{
    private readonly ProdutoService _produtoService;

    public ProdutoController(ProdutoService produtoService) =>
        _produtoService = produtoService;


    [HttpGet]
    public async Task<List<Produto>> Get() => await _produtoService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> Get(string id)
    {
        var produto = await _produtoService.GetAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Post(Produto produto)
    {
        await _produtoService.CreateAsync(produto);
        return CreatedAtAction("Get", new { id = produto.id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Produto produto)
    {
        var existingProduto = await _produtoService.GetAsync(id);
        if (existingProduto == null)
        {
            return NotFound();
        }

        produto.id = existingProduto.id;
        await _produtoService.UpdateAsync(id, produto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var produto = await _produtoService.GetAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        await _produtoService.DeleteAsync(id);

        return NoContent();
    }
}