using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Manager
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
