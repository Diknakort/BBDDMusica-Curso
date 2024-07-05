using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components
{
    public class ArtistasGrupoViewComponent(IRepositoryGenerico<Artista> Artistas, IRepositoryGenerico<Colaboracione> Colabs, IRepositoryGenerico<Role> Roles) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int grupoId)
        {
            var items = await Artistas.Lista()!;
            var itemsCol = await Colabs.Lista()!;
            var itemsFiltrados = itemsCol.Where(x => x.GruposId == grupoId);
            var artistasGrupo = await Artistas.Lista()!;
            artistasGrupo.Clear();
            var roles = await Roles.Lista()!;
            foreach (var item in itemsFiltrados)
            {
                var artista = items.Where(x => x.Id == item.ArtistasId).FirstOrDefault();
                artista!.RolPrincipalNavigation = roles.Where(x => x.Id == artista.RolPrincipal).FirstOrDefault();
                artistasGrupo.Add(artista);
            }
            return View(artistasGrupo);
        }
    }
}
