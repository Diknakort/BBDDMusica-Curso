using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models;

public partial class Grupo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? FechaCreaccion { get; set; }

    public int? ManagersId { get; set; }

    public virtual ICollection<Colaboracione> Colaboraciones { get; set; } = new List<Colaboracione>();

    public virtual ICollection<Concierto> Conciertos { get; set; } = new List<Concierto>();

    public virtual ICollection<Disco> Discos { get; set; } = new List<Disco>();

    public virtual Manager? Managers { get; set; }
}
