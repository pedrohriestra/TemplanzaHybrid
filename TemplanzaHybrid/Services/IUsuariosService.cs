using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public interface IUsuariosService
    {
        Task<IReadOnlyList<Usuario>> GetAllAsync(string? filtro = null);
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> AddAsync(Usuario user);
        Task<bool> UpdateAsync(Usuario user);
        Task<bool> DeleteAsync(int id);
    }
}
