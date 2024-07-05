using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataCancione))]
    public partial class Cancione { }
    public class MetaDataCancione
    {
        public int Id { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [DataType(DataType.Duration)]
        public TimeOnly? Duracion { get; set; }
        [Required]
        public int? DiscosId { get; set; }
    }
}
