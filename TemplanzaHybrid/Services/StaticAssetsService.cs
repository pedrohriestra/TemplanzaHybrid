using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplanzaHybrid.Services
{
    internal sealed class StaticAssetsService : IStaticAssetsService
    {
        private record ImageIndex(
            [property: JsonPropertyName("productos")] List<string> Productos,
            [property: JsonPropertyName("usuarios")] List<string> Usuarios);

        private ImageIndex _cache = new(new(), new());
        private bool _loaded;

        public async Task<IReadOnlyList<string>> GetProductoImagesAsync()
        {
            await EnsureLoadedAsync();
            return _cache.Productos;
        }

        public async Task<IReadOnlyList<string>> GetUsuarioImagesAsync()
        {
            await EnsureLoadedAsync();
            return _cache.Usuarios;
        }

        private async Task EnsureLoadedAsync()
        {
            if (_loaded) return;
            _loaded = true;

            // 1) Preferimos index.json empaquetado
            try
            {
                const string path = "wwwroot/images/index.json";
                if (await FileSystem.AppPackageFileExistsAsync(path))
                {
                    using var s = await FileSystem.OpenAppPackageFileAsync(path);
                    using var r = new StreamReader(s);
                    var json = await r.ReadToEndAsync();
                    _cache = JsonSerializer.Deserialize<ImageIndex>(json) ?? _cache;
                    return;
                }
            }
            catch { /* ignoramos y probamos fallback */ }

#if WINDOWS
            // 2) Fallback (Windows): enumerar el directorio del paquete
            try
            {
                var root = AppContext.BaseDirectory;

                static IEnumerable<string> ListImgs(string dir, string prefix)
                    => Directory.Exists(dir)
                        ? Directory.GetFiles(dir)
                                  .Where(IsImg)
                                  .Select(f => $"{prefix}/{Path.GetFileName(f)}")
                        : Enumerable.Empty<string>();

                var prodDir = Path.Combine(root, "wwwroot", "images", "productos");
                var usrDir  = Path.Combine(root, "wwwroot", "images", "usuarios");

                _cache = new ImageIndex(
                    ListImgs(prodDir, "/images/productos").ToList(),
                    ListImgs(usrDir,  "/images/usuarios").ToList()
                );
            }
            catch { /* nos quedamos con listas vacías */ }
#endif
        }

        private static bool IsImg(string f) =>
            f.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);
    }
}
