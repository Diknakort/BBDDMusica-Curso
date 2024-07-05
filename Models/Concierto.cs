using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Models;

public partial class Concierto
{
    public int Id { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Ciudad { get; set; }

    public int? GruposId { get; set; }

    public int? GirasId { get; set; }

    public virtual Gira? Giras { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
