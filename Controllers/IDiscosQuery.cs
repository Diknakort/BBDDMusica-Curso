using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers
{
    public interface IDiscosQuery
    {
        IEnumerable<Disco> dameDiscos(IEnumerable<Disco> Discos);
    }
}
