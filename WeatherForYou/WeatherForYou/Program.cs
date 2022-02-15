using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Concrete.Repositories;
using WeatherForYou.Domain.Concrete.Services;
using WeatherForYou.Domain.Contexts;
using WeatherForYou.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)),
        ServiceLifetime.Singleton);
builder.Services.AddSingleton<IRepository<MeteorologyData>, MetereologyRepository>();
builder.Services.AddSingleton<IDataLoader, ExcelDataLoader>();
builder.Services.AddSingleton<IDataProcessor, DataProcessor>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
