﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAO
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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

        [Required]
        public bool IsActive { get; set; } = true;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [RegularExpression("[MF]", ErrorMessage = "El sexo debe ser 'M' para Masculino o 'F' para Femenino")]
        public char Sexo { get; set; }
    }
}
