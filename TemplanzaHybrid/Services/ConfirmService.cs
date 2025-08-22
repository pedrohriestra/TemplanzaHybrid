using Microsoft.JSInterop;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

namespace TemplanzaHybrid.Services
{
    // IMPORTANTE: registrar como Scoped (no Singleton)
    public class ConfirmService : IConfirmService
    {
        private readonly IJSRuntime _js;
        public ConfirmService(IJSRuntime js) => _js = js;

        public async Task<bool> ConfirmAsync(string? message = null)
        {
            try
            {
                // Usa tu window.templanza.confirm si hay contexto JS
                return await _js.InvokeAsync<bool>("templanza.confirm", message);
            }
            catch (JSDisconnectedException)
            {
                return await NativeConfirmAsync(message);
            }
            catch (InvalidOperationException)
            {
                // "Cannot invoke JavaScript outside of a WebView context."
                return await NativeConfirmAsync(message);
            }
        }

        private static Task<bool> NativeConfirmAsync(string? message)
        {
            var page = Application.Current?.MainPage;
            if (page is null) return Task.FromResult(true);

            return MainThread.InvokeOnMainThreadAsync(
                () => page.DisplayAlert("Confirmación",
                                        message ?? "¿Confirmás?",
                                        "Sí", "No"));
        }
    }
}
