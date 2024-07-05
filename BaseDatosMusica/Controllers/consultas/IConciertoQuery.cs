using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public interface IConciertoQuery
    {
        IEnumerable<Concierto> dameConciertos(IEnumerable<Concierto> Conciertos);
    }
}
