using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Disco
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? Fecha { get; set; }

    public int? GeneroId { get; set; }

    public int? GruposId { get; set; }

    public virtual ICollection<Cancione> Canciones { get; set; } = new List<Cancione>();

    public virtual Genero? Genero { get; set; }

    public virtual Grupo? Grupos { get; set; }
}
