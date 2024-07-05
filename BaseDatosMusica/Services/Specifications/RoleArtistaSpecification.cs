using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specifications
{
    public class RoleArtistaSpecification<T>(int RoleId) : ISpecification<Artista>
    {
        public bool IsValid(Artista elemento)
        {
            return elemento.RolPrincipal == RoleId;
        }
    }
}
