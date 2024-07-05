using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Artista
{
    public int Id { get; set; }

    public string? NombreReal { get; set; }

    public string? NombreArtistico { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? FechaNacimiento { get; set; }

    public int? RolPrincipal { get; set; }

    public virtual ICollection<Colaboracione> Colaboraciones { get; set; } = new List<Colaboracione>();

    public virtual Role? RolPrincipalNavigation { get; set; }
}
