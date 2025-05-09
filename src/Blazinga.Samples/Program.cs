using System.Globalization;
using Blazinga.Samples;
using Blazinga.Samples.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ThemeService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var host = builder.Build();

var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("localStorage.getItem", "culture");
var culture = result is not null ? new CultureInfo(result) : new CultureInfo("en");

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
