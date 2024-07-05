using BaseDatosMusica.Models;
using System.Collections.Generic;

namespace BaseDatosMusica.Services.FakeRepository
{
    public class FakeRepository : IManagersRepository
    {
        public List<Manager> ListaManagers = new();

        public FakeRepository()
        {
            Manager miManager = new()
            {
                FechaNacimiento = new DateOnly(1980, 5, 12),
                Grupos = new List<Grupo>(),
                Nombre = "Melissa"
            };
            ListaManagers.Add(miManager);
            miManager = new()
            {
                FechaNacimiento = new DateOnly(1990, 2, 12),
                Grupos = new List<Grupo>(),
                Nombre = "anggeld"
            };
            ListaManagers.Add(miManager);
            miManager = new()
            {
                FechaNacimiento = new DateOnly(1976, 1, 22),
                Grupos = new List<Grupo>(),
                Nombre = "german"
            };
            ListaManagers.Add(miManager);
            miManager = new()
            {
                FechaNacimiento = new DateOnly(1992, 7, 12),
                Grupos = new List<Grupo>(),
                Nombre = "pedro"
            };
            ListaManagers.Add(miManager);
        }


        public List<Manager> dameManagers()

        {
            return ListaManagers;
        }

        public Manager? DameUno(int Id)
        {
            return ListaManagers.FirstOrDefault(x => x.Id == Id);
        }

        public bool BorrarManager(int Id)
        {
            return ListaManagers.Remove(DameUno(Id));
        }

        public bool Agregar(Manager Manager)
        {
            ListaManagers.Add(Manager);
            return true;
        }

        public void Modificar(int Id, Manager Manager)
        {
            BorrarManager(Id);
            Agregar(Manager);
        }
    }
}
