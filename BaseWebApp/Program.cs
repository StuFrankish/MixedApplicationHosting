var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// MVC & Razor endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapControllers();

// Blazor WASM under /admin
app.Map("/admin", adminApp =>
{
    adminApp.UseBlazorFrameworkFiles();
    adminApp.UseStaticFiles();

    adminApp.UseRouting();
    adminApp.UseAuthorization();

    adminApp.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("{*path:nonfile}", "index.html");
    });
});

app.Run();
