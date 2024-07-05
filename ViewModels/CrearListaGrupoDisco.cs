using BaseDatosMusica.Models;

namespace BaseDatosMusica.ViewModels
{
    public class CrearListaGrupoDisco: ICrearListaDiscosPorGrupo
    {
        private readonly GrupoDContext _context;
        private readonly IDiscoGrupoBuilder builder;

        public CrearListaGrupoDisco(GrupoDContext _context, IDiscoGrupoBuilder builder)
        {
            this._context= _context;
            this.builder= builder;
        }

        public List<DiscosPorGrupoViewModel> DameDiscosGrupo()
        {
            var resultado = from p in _context.Discos.ToList()
                group (p) by p.Grupos into g
                select g.Key;
            List<DiscosPorGrupoViewModel> coleccionADevolver = new();
            foreach (var g in resultado)
            {
                DiscosPorGrupoViewModel grupos = new()
                {
                    Id= g.Id,
                    grupo = g,
                    DiscosGrupos = new DiscoGrupoObjeto(_context).listaDiscos(g).ToList()
                };
                coleccionADevolver.Add(grupos);
            }

            return coleccionADevolver;
        }
    }
}
