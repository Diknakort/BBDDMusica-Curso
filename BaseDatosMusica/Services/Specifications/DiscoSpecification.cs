using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specifications
{
    public class DiscoSpecification<T>(int DiscoId) : ISpecification<Cancione>
    {
        public bool IsValid(Cancione elemento)
        {
            return elemento.DiscosId == DiscoId;
        }
    }
}
