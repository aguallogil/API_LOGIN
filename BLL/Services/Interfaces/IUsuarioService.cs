using BLL.DTOs;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUsuarioService
    {
        Usuario CreateUsuario(Usuario usuarioDto);
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int id);
        void UpdateUsuario(Usuario usuarioDto);
        void DeleteUsuario(int id);
        Usuario AuthenticateUser(string username, string password);
    }
}
