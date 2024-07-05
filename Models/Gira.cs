using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Gira
{
    public int Id { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaFinal { get; set; }
    public object? Conciertos { get; internal set; }
}
