using BaseDatosMusica.Models;

namespace BaseDatosMusica.ViewModels
{
    public interface IDiscoGrupoBuilder
    {
        List<Disco> listaDiscos(Grupo Grupo);
    }
}
