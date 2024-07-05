using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataManager))]
    public partial class Manager { }
    public class MetaDataManager
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FechaNacimiento { get; set; }
    }
}
