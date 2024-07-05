using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public class Consulta80 : IArtistasQuery
    {
        public DateOnly fechaLimite = new DateOnly(1990, 01, 01);
        public IEnumerable<Artista> dameArtistas(IEnumerable<Artista> Artistas)
        {
            return Artistas.Where(x => x.FechaNacimiento < fechaLimite);
        }
    }
}
