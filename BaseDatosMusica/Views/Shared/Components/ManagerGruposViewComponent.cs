using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components
{
    public class ManagerGruposViewComponent(IRepositoryGenerico<Grupo> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int managers)
        {
            ISpecification<Grupo> especificacion = new ManagersGrupoSpecification<Grupo>(managers);
            var items = await coleccion.Lista()!;
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }

    }
}
