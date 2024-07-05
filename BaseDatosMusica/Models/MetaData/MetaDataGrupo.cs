using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaseDatosMusica.Models
{
    [ModelMetadataType(typeof(MetaDataGrupo))]
    public partial class Grupo { }
    public class MetaDataGrupo
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FechaCreaccion { get; set; }

        public int? ManagersId { get; set; }
    }
}
