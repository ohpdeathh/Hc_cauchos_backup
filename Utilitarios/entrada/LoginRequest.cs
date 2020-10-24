using System;
using System.ComponentModel.DataAnnotations;

namespace Utilitarios
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "el correo es requerido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "la contraseña es requerida.")]
        public string Password { get; set; }
        
        [Required]
        public int AplicacionId { get; set; }
    }
}