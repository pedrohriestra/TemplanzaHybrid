using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public interface IAuthService
    {
        event Action? AuthenticationStateChanged;

        bool IsAuthenticated { get; }
        Usuario? UsuarioActual { get; }
        bool IsInRole(RolUsuario rol);

        Task<bool> SignInAsync(string email, string password);
        Task SignOutAsync();

        // Registro con autologin (rol Cliente por defecto)
        Task<(bool ok, string? error)> RegisterAsync(
            string nombre,
            string email,
            string password,
            string? imagenUrl = null);
    }
}
