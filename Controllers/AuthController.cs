using IfoodParaguai.Services;
using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Controllers;
[ApiController]
[Route("/v1/[controller]")]
public class AuthController : Controller
{
    private readonly UserService _userService;
    public AuthController(UserService userService) => _userService = userService;
    
    [HttpPost]
    public async Task<IActionResult> Auth(string email, string password)
    {
        var usuario = await _userService.Verify(email, password);
        if (usuario != null)
        {
            var token = TokenService.GenerateToken(usuario);
            return Ok(token);
        }

        return BadRequest("Usuario ou senha invalidos.");
    }
}
