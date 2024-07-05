using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers
{
    public interface IConciertoQuery
    {
        IEnumerable<Concierto> dameConciertos(IEnumerable<Concierto> Conciertos);
    }
}
