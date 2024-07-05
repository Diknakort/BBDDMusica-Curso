namespace BaseDatosMusica.Models;

public partial class Gira
{
    public int Id { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaFinal { get; set; }
    public List<Concierto>? Conciertos { get; internal set; }
}
