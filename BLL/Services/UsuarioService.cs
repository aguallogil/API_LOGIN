using BLL.Services.Interfaces;
using DAL.Interfaces;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public  class UsuarioService:IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Usuario CreateUsuario(Usuario usuarioDto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Email = usuarioDto.Email,
                    UserName = usuarioDto.UserName,
                    Password = this.HashPassword(usuarioDto.Password),
                    Sexo = usuarioDto.Sexo,
                };

                this._unitOfWork.Usuarios.Add(usuario);
                this._unitOfWork.Complete();

                usuario.Password = string.Empty;
                return usuario;
            }
            catch (DbUpdateException ex)
            {
 
                throw new InvalidOperationException("No se pudo crear el usuario, el nombre de usuario ya esté en uso.", ex);
            }
        }


        public IEnumerable<Usuario> GetAllUsuarios()
        {
            var usuarios = this._unitOfWork.Usuarios.GetAll();
            return usuarios;
        }

        public Usuario GetUsuarioById(int id)
        {
            var usuario = this._unitOfWork.Usuarios.Get(id);
            return usuario;
        }

        public void UpdateUsuario(Usuario usuarioDto)
        {
            var usuario = this._unitOfWork.Usuarios.Get(usuarioDto.Id);
            if (usuario != null)
            {
                usuario.Email = usuarioDto.Email;
                usuario.UserName = usuarioDto.UserName;
                usuario.Sexo = usuarioDto.Sexo;
                usuario.Password= this.HashPassword(usuarioDto.Password);
                this._unitOfWork.Complete();
            }
            else
            {
                throw new KeyNotFoundException("Usuario no encontrado con el ID especificado.");
            }
        }

        public void DeleteUsuario(int id)
        {
            var usuario = this._unitOfWork.Usuarios.Get(id);
            if (usuario != null)
            {
                usuario.IsActive= false;
                this._unitOfWork.Complete();
            }
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public Usuario AuthenticateUser(string username, string password)
        {
            var usuario = _unitOfWork.Usuarios.Find(u => u.UserName == username && u.IsActive).FirstOrDefault();

            if (usuario != null && BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                return usuario;
            }
            return null;
        }
    }
}
