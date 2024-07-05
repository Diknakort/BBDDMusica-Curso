namespace BaseDatosMusica.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Artista> Artista { get; set; } = new List<Artista>();
}
