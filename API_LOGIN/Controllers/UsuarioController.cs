using BLL.Services.Interfaces;
using DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_LOGIN.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            var usuarios = _usuarioService.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _usuarioService.GetUsuarioById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            var createdUsuario = _usuarioService.CreateUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.Id }, createdUsuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest("ID de URL no coincide con ID del cuerpo");

            _usuarioService.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            _usuarioService.DeleteUsuario(id);
            return NoContent();
        }
    }
}
