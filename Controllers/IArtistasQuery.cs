using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers
{
    public interface IArtistasQuery
    {
        IEnumerable<Artista> dameArtistas(IEnumerable<Artista> Artistas);
    }
}
