namespace TemplanzaHybrid.Services
{
    public interface IStaticAssetsService
    {
        Task<IReadOnlyList<string>> GetProductoImagesAsync();
        Task<IReadOnlyList<string>> GetUsuarioImagesAsync();
    }
}
