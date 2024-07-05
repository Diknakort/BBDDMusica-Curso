using BaseDatosMusica.Models;

namespace BaseDatosMusica.ViewModels;

public class TourWithConcertsViewModel
{
    public Gira? Tour { get; set; }
    public List<Concierto>? Concerts { get; set; }
}