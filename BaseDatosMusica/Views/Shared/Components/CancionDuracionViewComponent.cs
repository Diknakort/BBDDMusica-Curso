using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace BaseDatosMusica.Views.Shared.Components
{
    public class CancionDuracionViewComponent : ViewComponent
    {
        public Task<string> InvokeAsync(Cancione cancion)
        {
            var time = new TimeOnly(0, 0);
            if (cancion.Duracion.Equals(time)  || cancion.Duracion==null)
            {
                return Task.FromResult("");
            }
            else
            {
                return Task.FromResult(cancion.Titulo + " (" + cancion.Duracion + ")");
            }
        }
    }
}
//Vais a crear un servicio para que pasandole el objeto canción devuelva una cadena de texto,
//con el título y entre paréntesis la duración, si la duración es cero o null, la cadena de
//texto debe de aparecer vacía.