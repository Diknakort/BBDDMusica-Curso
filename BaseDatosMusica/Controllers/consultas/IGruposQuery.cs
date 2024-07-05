using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public interface IGruposQuery
    {
        IEnumerable<Grupo> dameGrupos(IEnumerable<Grupo> Grupos);
    }
}