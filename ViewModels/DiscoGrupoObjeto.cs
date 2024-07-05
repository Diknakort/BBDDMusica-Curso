using BaseDatosMusica.Models;

namespace BaseDatosMusica.ViewModels
{
    public class DiscoGrupoObjeto : IDiscoGrupoBuilder
    {
        private readonly GrupoDContext _context;
        public DiscoGrupoObjeto(GrupoDContext context)
        {
            this._context = context;
        }

        public List<Disco> listaDiscos(Grupo Grupo)
        {
            var resultado =
                from d in this._context.Discos
                join g in this._context.Grupos
                    on d.GruposId equals g.Id
                where g == Grupo
                select d;
            return resultado.ToList();
        }
    }
}
