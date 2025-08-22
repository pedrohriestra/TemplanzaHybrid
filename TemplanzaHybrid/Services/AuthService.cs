using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuariosService _usuarios;
        private Usuario? _actual;

        public AuthService(IUsuariosService usuarios) => _usuarios = usuarios;

        public event Action? AuthenticationStateChanged;

        public bool IsAuthenticated => _actual != null;
        public Usuario? UsuarioActual => _actual;
        public bool IsInRole(RolUsuario rol) => _actual?.Rol == rol;

        public async Task<bool> SignInAsync(string email, string password)
        {
            var list = await _usuarios.GetAllAsync();
            var match = list.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password &&
                u.Activo);

            if (match is null) return false;
            _actual = match;
            AuthenticationStateChanged?.Invoke();
            return true;
        }

        public Task SignOutAsync()
        {
            _actual = null;
            AuthenticationStateChanged?.Invoke();
            return Task.CompletedTask;
        }

        public async Task<(bool ok, string? error)> RegisterAsync(
            string nombre, string email, string password, string? imagenUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
                return (false, "Completá nombre, email y contraseña.");

            var existentes = await _usuarios.GetAllAsync();
            if (existentes.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return (false, "El email ya está registrado.");

            var nuevo = new Usuario
            {
                Nombre = nombre.Trim(),
                Email = email.Trim(),
                Password = password,         // (en prod, hashear)
                Rol = RolUsuario.Usuario,
                ImagenUrl = imagenUrl,
                Activo = true
            };

            try
            {
                await _usuarios.AddAsync(nuevo);
                _actual = nuevo;             // autologin
                AuthenticationStateChanged?.Invoke();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
