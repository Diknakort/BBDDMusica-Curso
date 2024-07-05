using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public interface IArtistasQuery
    {
        IEnumerable<Artista> dameArtistas(IEnumerable<Artista> Artistas);

    }
}
