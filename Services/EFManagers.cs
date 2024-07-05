using BaseDatosMusica.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Services
{
    public class EFManagers : IManagersRepository
    {
        private readonly GrupoDContext _context = new();
        public List<Manager> dameManagers()
        {
            return _context.Managers.AsNoTracking().ToList();
        }

        public Manager? DameUno(int Id)
        {
            return _context.Managers.Find(Id);
        }

        public bool BorrarManager(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Managers.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Agregar(Manager Manager)
        {
            if (DameUno(Manager.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Managers.Add(Manager);
                _context.SaveChanges();
                return true;
            }
        }

        public void Modificar(int Id, Manager Manager)
        {
            var modifica = DameUno(Id);
            if (modifica != null)
            {
                BorrarManager(Id);
            }

            Agregar(Manager);
        }
    }
}
