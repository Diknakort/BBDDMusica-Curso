using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components
{
    public class ArtistasRolesViewComponent(IRepositoryGenerico<Artista> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Role)
        {
            ISpecification<Artista> especificacion = new RoleArtistaSpecification<Artista>(Role);
            var items = await coleccion.Lista()!;
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
