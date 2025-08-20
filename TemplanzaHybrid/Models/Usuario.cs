using System.ComponentModel.DataAnnotations;

namespace TemplanzaHybrid.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(80, MinimumLength = 2)]
        public string Nombre { get; set; } = "";

        [Required, EmailAddress, StringLength(120)]
        public string Email { get; set; } = "";

        [Required, StringLength(60, MinimumLength = 4)]
        public string Password { get; set; } = ""; // simple para el TP

        [Required]
        public RolUsuario Rol { get; set; } = RolUsuario.Usuario;

        [Display(Name = "Imagen"), StringLength(200)]
        public string? ImagenUrl { get; set; } // ej: /images/usuarios/admin.webp

        public bool Activo { get; set; } = true;
    }
}
