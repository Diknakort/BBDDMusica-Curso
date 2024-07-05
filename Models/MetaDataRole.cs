using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataRole))]
    public partial class Role { }
    public class MetaDataRole
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FechaNacimiento { get; set; }
    }
}
