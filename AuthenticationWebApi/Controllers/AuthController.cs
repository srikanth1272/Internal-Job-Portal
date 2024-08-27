using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string GenerateJWT(string userName, string role, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey,
                                SecurityAlgorithms.HmacSha256);
            Claim[] claims = new Claim[]{
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
            };
            var token = new JwtSecurityToken(
                issuer: "http://www.allcloud.in",
                audience: "http://www.allcloud.in",
                expires: DateTime.Now.AddHours(2),
                claims: claims,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("{userName}/{role}/{secretKey}")]
        public ActionResult GetToken(string userName, string role, string secretKey)
        {
            string token = GenerateJWT(userName, role, secretKey);
            return Ok(token);
        }
    }
}
