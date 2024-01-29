// Using directives
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWasm.Auth;
using BlazorWASM.Services;
using BlazorWASM.Services.Http;
using Domain.Auth;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services to the container
builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri("https://localhost:7081") }
);
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<ITodoService, TodoHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
AuthorizationPolicies.AddPolicies(builder.Services);

// Build and run the application
await builder.Build().RunAsync();