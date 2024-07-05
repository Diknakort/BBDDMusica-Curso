using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public class ConsultaConciertoscs : IConciertoQuery
    {
        public IEnumerable<Concierto> dameConciertos(IEnumerable<Concierto> Conciertos)
        {
            return Conciertos.Where(x => x.Giras == null);
        }
    }
}
