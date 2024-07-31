using LogSearchConsole.Services;
using LogSearchConsole.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ILogSearchService, LogSearchService>(o => new LogSearchService(builder.Configuration["FileDirectory"]));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
