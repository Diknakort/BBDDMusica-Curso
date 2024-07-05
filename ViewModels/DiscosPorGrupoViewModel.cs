using BaseDatosMusica.Models;

namespace BaseDatosMusica.ViewModels
{
    public class DiscosPorGrupoViewModel
    {
        public int Id { get; set; }
        public Grupo  grupo {get; set;}
        
        public List<Disco> DiscosGrupos {get; set;}
    }
}
