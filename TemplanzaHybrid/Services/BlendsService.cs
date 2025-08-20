using TemplanzaHybrid.Models;

namespace TemplanzaHybrid.Services
{
    public class BlendsService : IBlendsService
    {
        private readonly List<Blend> _items = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public BlendsService()
        {
            _items.AddRange(new[]
            {
                new Blend { Id=_nextId++, Nombre="Té Negro Vainilla", Tipo="Negro",
                            Precio=3500, Stock=20,
                            ImagenUrl="/images/productos/negro_vainilla.webp" },
                new Blend { Id=_nextId++, Nombre="Green Citrus", Tipo="Verde",
                            Precio=3200, Stock=15,
                            ImagenUrl="/images/productos/green_citrus.webp" },
            });
        }

        public Task<IReadOnlyList<Blend>> GetAllAsync(string? filtro = null)
        {
            IEnumerable<Blend> q = _items;
            if (!string.IsNullOrWhiteSpace(filtro))
                q = q.Where(p => p.Nombre.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                              || (p.Tipo ?? "").Contains(filtro, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult((IReadOnlyList<Blend>)q.OrderBy(p => p.Id).ToList());
        }

        public Task<Blend?> GetByIdAsync(int id)
            => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

        public Task<Blend> AddAsync(Blend item)
        {
            lock (_lock) { item.Id = _nextId++; _items.Add(item); }
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(Blend item)
        {
            lock (_lock)
            {
                var idx = _items.FindIndex(x => x.Id == item.Id);
                if (idx < 0) return Task.FromResult(false);
                _items[idx] = item; return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_lock)
            {
                var e = _items.FirstOrDefault(x => x.Id == id);
                if (e == null) return Task.FromResult(false);
                _items.Remove(e); return Task.FromResult(true);
            }
        }
    }
}
