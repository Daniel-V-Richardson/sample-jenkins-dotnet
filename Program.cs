using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ABPFramworkProject;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseAutofac();
builder.Services.ReplaceConfiguration(builder.Configuration);
await builder.AddApplicationAsync<AppModule>();
builder.Services.AddRazorPages();

// Configure session services
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourSessionCookieName";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout as needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/current-user", ([FromServices] HelloService helloService) =>
{
    return helloService.SayHi();
});

app.MapGet("/fetch-from-checklist", async ([FromServices] HelloService helloService) => {
    return await helloService.FetchFromChecklist();
});

await app.InitializeApplicationAsync();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Enable session
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

await app.RunAsync();