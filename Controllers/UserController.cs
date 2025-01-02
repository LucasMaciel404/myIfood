using IfoodParaguai.Models;
using IfoodParaguai.PassHash;
using IfoodParaguai.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IfoodParaguai.Controllers;

[ApiController]
[Route("/v1/[controller]/")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService) =>
        _userService = userService;

    [HttpGet]
    public async Task<List<RetornoUsuario>> Get()
    {
        try
        {
            var usuarios = await _userService.GetAsync();
            List<RetornoUsuario> usersSemPassword = usuarios.Select(user => new RetornoUsuario()
            {
                Id = user.Id.ToString(),
                email = user.email,
                idade = user.idade,
                nome = user.nome
            }).ToList();
            return usersSemPassword;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar usuários: {ex.Message}");
            throw;
        }
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<RetornoUsuario>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(new RetornoUsuario()
        {
            Id = user.Id.ToString(),
            nome = user.nome,
            email = user.email,
            idade = user.idade
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequisicaoUsuario newUser)
    {
        await _userService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [Authorize]
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, RequisicaoUsuario updatedUser)
    {
        var user = await _userService.GetAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        CriptografarSenha hasher = new CriptografarSenha();
        string salt = hasher.GerarSalt();
        string hash = hasher.GerarHash(updatedUser.password, salt);

        Usuario userUpdate = new Usuario()
        {
            Id = user.Id,
            nome = updatedUser.nome ?? user.nome,
            email = updatedUser.email ?? user.email,
            idade = updatedUser.idade > 0 ? updatedUser.idade : user.idade,
            hash = hash,
            salt = salt
        };

        if (userUpdate != null)
        {
            Console.WriteLine("Usuário a ser atualizado");
            await _userService.UpdateAsync(user.Id, userUpdate);
        }

        return NoContent();
    }


    [Authorize]
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }
}
