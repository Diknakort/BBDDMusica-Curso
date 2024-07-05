using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services
{
    public interface IManagersRepository
    {
        public List<Manager> dameManagers();
        public Manager? DameUno(int Id);
        public bool BorrarManager(int Id);
        public bool Agregar(Manager Manager);
        public void Modificar(int Id, Manager Manager);
    }
}
