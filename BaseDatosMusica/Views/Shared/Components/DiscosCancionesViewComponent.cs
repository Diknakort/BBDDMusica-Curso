using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components
{
    public class DiscosCancionesViewComponent(IRepositoryGenerico<Cancione> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Disco)
        {
            ISpecification<Cancione> especificacion = new DiscoSpecification<Cancione>(Disco);
            var items = await coleccion.Lista()!;
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }

    }
}
