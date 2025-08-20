using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly List<Usuario> _usuarios = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public UsuariosService()
        {
            _usuarios.AddRange(new[]
            {
                new Usuario { Id=_nextId++, Nombre="Admin", Email="admin@demo.com",
                              Password="admin123", Rol=RolUsuario.Admin,
                              ImagenUrl="/images/usuarios/admin.webp" },
                new Usuario { Id=_nextId++, Nombre="Pedro", Email="Pedro@demo.com",
                              Password="1234", Rol=RolUsuario.Usuario,
                              ImagenUrl="/images/usuarios/juana.webp" },
            });
        }

        public Task<IReadOnlyList<Usuario>> GetAllAsync(string? filtro = null)
        {
            IEnumerable<Usuario> q = _usuarios;
            if (!string.IsNullOrWhiteSpace(filtro))
                q = q.Where(u => u.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                              || u.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult((IReadOnlyList<Usuario>)q.OrderBy(u => u.Id).ToList());
        }

        public Task<Usuario?> GetByIdAsync(int id)
            => Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));

        public Task<Usuario> AddAsync(Usuario user)
        {
            lock (_lock) { user.Id = _nextId++; _usuarios.Add(user); }
            return Task.FromResult(user);
        }

        public Task<bool> UpdateAsync(Usuario user)
        {
            lock (_lock)
            {
                var idx = _usuarios.FindIndex(u => u.Id == user.Id);
                if (idx < 0) return Task.FromResult(false);
                _usuarios[idx] = user; return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_lock)
            {
                var u = _usuarios.FirstOrDefault(u => u.Id == id);
                if (u == null) return Task.FromResult(false);
                _usuarios.Remove(u); return Task.FromResult(true);
            }
        }
    }
}
