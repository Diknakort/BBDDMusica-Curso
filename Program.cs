using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.FakeRepository;
using BaseDatosMusica.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GrupoDContext>(
       options => options.UseSqlServer("\"server=musicagrupos.database.windows.net;database=GrupoD;user=as;password=P0t@t0P0t@t0\""));
builder.Services.AddScoped<ICrearListaDiscosPorGrupo, CrearListaGrupoDisco>();
builder.Services.AddScoped<IDiscoGrupoBuilder, DiscoGrupoObjeto>();
builder.Services.AddScoped<IManagersRepository, EFManagers>();
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
