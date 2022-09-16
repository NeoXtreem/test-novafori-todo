using Microsoft.EntityFrameworkCore;
using NovaFori.Todos.Data;
using NovaFori.Todos.Services;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Todos.db");

// Add services to the container.
builder
    .Services.AddControllersWithViews()
    .Services.AddDbContext<TodosContext>(options => options.UseSqlite($"Data Source={dbPath}"))
    .AddScoped<TodosService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<TodosContext>());
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html"); ;

app.Run();