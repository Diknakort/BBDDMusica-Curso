using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataGenero))]
    public partial class Genero { }
    public class MetaDataGenero
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("")]
        public string? Nombre { get; set; }

    }
}
