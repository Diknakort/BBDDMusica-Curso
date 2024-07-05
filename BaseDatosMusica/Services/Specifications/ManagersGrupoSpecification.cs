using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.Specifications
{
    public class ManagersGrupoSpecification<T>(int ManagersId) : ISpecification<Grupo>
    {
        public bool IsValid(Grupo elemento)
        {
            return elemento.ManagersId == ManagersId;
        }
    }
}
