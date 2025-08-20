using Microsoft.JSInterop;

namespace TemplanzaHybrid.Services
{
    public class ConfirmService : IConfirmService
    {
        private readonly IJSRuntime _js;
        public ConfirmService(IJSRuntime js) => _js = js;
        public Task<bool> ConfirmAsync(string message)
            => _js.InvokeAsync<bool>("templanza.confirm", message).AsTask();
    }
}
