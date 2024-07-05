using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataColaboracione))]
    public partial class Colaboracione { }
    public class MetaDataColaboracione
    {
        public int Id { get; set; }
        [Required]
        public int? GruposId { get; set; }
        [Required]
        public int? ArtistasId { get; set; }
    }
}
