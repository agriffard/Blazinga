using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazinga.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task<CultureInfo> GetCurrentCultureAsync(this WebAssemblyHost host)
    {
        var js = host.Services.GetRequiredService<IJSRuntime>();
        var result = await js.InvokeAsync<string>("localStorage.getItem", "culture");
        var culture = result is not null ? new CultureInfo(result) : new CultureInfo("en");

        return culture;
    }
}
