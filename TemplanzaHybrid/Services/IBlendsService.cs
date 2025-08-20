using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public interface IBlendsService
    {
        Task<IReadOnlyList<Blend>> GetAllAsync(string? filtro = null);
        Task<Blend?> GetByIdAsync(int id);
        Task<Blend> AddAsync(Blend item);
        Task<bool> UpdateAsync(Blend item);
        Task<bool> DeleteAsync(int id);
    }
}
