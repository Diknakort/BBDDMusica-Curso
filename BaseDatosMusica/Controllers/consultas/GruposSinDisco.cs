using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public class GruposSinDisco : IGruposQuery
    {
        private readonly GrupoDContext _context;

        public GruposSinDisco(GrupoDContext context)
        {
            _context = context;
        }

        public IEnumerable<Grupo> dameGrupos(IEnumerable<Grupo> Grupos)
        {
            var gruposConDiscos = _context.Discos.Select(d => d.GruposId).Distinct().ToList();
            return Grupos.Where(g => !gruposConDiscos.Contains(g.Id)).ToList();
        }
    }
}