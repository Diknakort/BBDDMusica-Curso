﻿using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers.consultas
{
    public class ConsultasKpop : IDiscosQuery
    {
        public IEnumerable<Disco> dameDiscos(IEnumerable<Disco> Discos)
        {
            return Discos.Where(x => x.GeneroId == 1);
        }
    }
}
