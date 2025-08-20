namespace TemplanzaHybrid.Services
{
    public interface IConfirmService
    {
        Task<bool> ConfirmAsync(string message);
    }
}