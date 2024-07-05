using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public interface IDiscosQuery
    {
        IEnumerable<Disco> dameDiscos(IEnumerable<Disco> Discos);
    }
}
