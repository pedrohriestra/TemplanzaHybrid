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
    }
}
