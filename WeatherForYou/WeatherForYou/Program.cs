using Microsoft.EntityFrameworkCore;
using WeatherForYou.Domain.Abstract;
using WeatherForYou.Domain.Concrete.Repositories;
using WeatherForYou.Domain.Concrete.Services;
using WeatherForYou.Domain.Contexts;
using WeatherForYou.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MeteorologyContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(MeteorologyContext).Assembly.FullName)));
builder.Services.AddScoped<IRepository<MeteorologyData>, MetereologyRepository>();
builder.Services.AddScoped<IDataLoader, ExcelDataLoader>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
