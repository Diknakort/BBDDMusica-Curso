using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models.MetaData
{
    [ModelMetadataType(typeof(GirasMetadata))]
    public partial class Giras { }
    public class GirasMetadata
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? Fecha_Inicio { get; set; }
        [DisplayName("Nombre de Gira")]
        public string? Nombre { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? Fecha_Final { get; set; }
    }
}
