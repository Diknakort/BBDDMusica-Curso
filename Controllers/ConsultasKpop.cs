using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers
{
    public class ConsultasKpop : IDiscosQuery
    {
        public IEnumerable<Disco> dameDiscos(IEnumerable<Disco> Discos)
        {
            return Discos.Where(x => x.GeneroId==1);
        }
    }
}
