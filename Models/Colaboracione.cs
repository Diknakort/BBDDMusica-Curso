using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Models;

public partial class Colaboracione
{
    public int Id { get; set; }

    public int? GruposId { get; set; }

    public int? ArtistasId { get; set; }

    public virtual Artista? Artistas { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
