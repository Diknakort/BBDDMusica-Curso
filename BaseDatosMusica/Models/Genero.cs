namespace BaseDatosMusica.Models;

public partial class Genero
{

    public int Id { get; set; }
    public string? Nombre { get; set; }

    public virtual ICollection<Disco> Discos { get; set; } = new List<Disco>();
}
