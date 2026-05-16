using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KooliProjekt.BlazorWASM;
using KooliProjekt.BlazorWASM.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5086/") });
builder.Services.AddScoped<IProductApiClient, ProductApiClient>();

await builder.Build().RunAsync();
