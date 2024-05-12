using BLL.DTOs;
using BLL.Services.Interfaces;
using DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_LOGIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]  LoginDTO loginDto)
        {
            var usuario = _usuarioService.AuthenticateUser(loginDto.Username, loginDto.Password);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            var tokenString = GenerateJwtToken(usuario);
            usuario.Password = string.Empty;
            return Ok(new { Token = tokenString, UserDetails = usuario });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim("UserId", usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
