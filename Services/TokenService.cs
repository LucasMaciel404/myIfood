using IfoodParaguai.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace IfoodParaguai.Services
{
    public class TokenService
    {
        public static object GenereteToken(Usuario Usuario) {

            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenSettings = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim("UsuarioId", Usuario.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHeadler = new JwtSecurityTokenHandler();
            var token = tokenHeadler.CreateToken(tokenSettings);
            var tokenString = tokenHeadler.WriteToken(token);

            return new
            {
                Token = tokenString
            };
            
        }
    }
}
