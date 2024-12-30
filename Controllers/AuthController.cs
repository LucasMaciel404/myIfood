using IfoodParaguai.Services;
using Microsoft.AspNetCore.Mvc;

namespace IfoodParaguai.Controllers;
[ApiController]
[Route("/v1/[controller]")]
public class AuthController : Controller
{
    [HttpPost]
    public IActionResult Auth(string username, string password)
    {
        if (username == "lucas" && password == "12345")
        {
            var token = TokenService.GenereteToken(new Models.Usuario() { Id = "123456"});
            return Ok(token);
        }

        return BadRequest("Usuario ou senha invalidos.");
    }
}
