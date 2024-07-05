using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataConcierto))]
    public partial class Concierto { }
    public class MetaDataConcierto
    {
        public int Id { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        [Required]
        public DateTime? FechaHora { get; set; }
        [Required]
        public string? Ciudad { get; set; }

        public int? GruposId { get; set; }

        public int? GirasId { get; set; }
    }
}
