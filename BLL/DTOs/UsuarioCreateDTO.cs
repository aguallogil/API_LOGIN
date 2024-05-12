using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "El nombre de usuario debe tener al menos 7 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,}$",
        ErrorMessage = "La contraseña debe tener al menos 10 caracteres y contener al menos una mayúscula, una minúscula, un número y un símbolo.")]
        public string Password { get; set; }
    }
}
