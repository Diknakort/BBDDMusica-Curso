using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Cancione
{
    public int Id { get; set; }

    public string? Titulo { get; set; }
    [DataType(DataType.Duration)]
    public TimeOnly? Duracion { get; set; }

    public int? DiscosId { get; set; }

    public virtual Disco? Discos { get; set; }
}
