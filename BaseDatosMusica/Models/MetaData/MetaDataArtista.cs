using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataArtista))]
    public partial class Artista { }
    public class MetaDataArtista
    {
        public int Id { get; set; }
        [Required]
        public string? NombreReal { get; set; }
        [Required]
        [MaxLength(30)]
        public string? NombreArtistico { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? FechaNacimiento { get; set; }

        [Required]
        public int? RolPrincipal { get; set; }
    }
}
