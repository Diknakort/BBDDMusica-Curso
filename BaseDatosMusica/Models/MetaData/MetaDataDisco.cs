using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataDisco))]
    public partial class Disco { }
    public class MetaDataDisco
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        public int? GeneroId { get; set; }

        public int? GruposId { get; set; }
    }
}
