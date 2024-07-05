using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services.FakeRepository
{
    public class FakeRepositoryCanciones : ICancionesRepository
    {
        public List<Cancione> ListaCanciones = new();

        public FakeRepositoryCanciones()
        {
            Cancione miCancion = new()
            {
                Id = 1,
                Discos = new Disco(),
                Duracion = new TimeOnly(0, 2, 20),
                Titulo = "pepinillo",
                DiscosId = 1
            };
            ListaCanciones.Add(miCancion);

            miCancion = new()
            {
                Id = 1,
                Discos = new Disco(),
                Duracion = new TimeOnly(0, 2, 20),
                Titulo = "pepinillo",
                DiscosId = 1
            };

            ListaCanciones.Add(miCancion);
            miCancion = new()
            {
                Id = 1,
                Discos = new Disco(),
                Duracion = new TimeOnly(0, 2, 20),
                Titulo = "pepinillo",
                DiscosId = 1
            };

            ListaCanciones.Add(miCancion);
            miCancion = new()
            {
                Id = 1,
                Discos = new Disco(),
                Duracion = new TimeOnly(0, 2, 20),
                Titulo = "pepinillo",
                DiscosId = 1
            };
            ListaCanciones.Add(miCancion);
        }

        public bool Agregar(Cancione cancion)
        {
            ListaCanciones.Add(cancion);
            return true;
        }

        public bool BorrarCancion(int Id)
        {
             return ListaCanciones.Remove(DameUna(Id));
        }

        public List<Cancione> dameCanciones()
        {
            return ListaCanciones;
        }

        public Cancione? DameUna(int Id)
        {
            return ListaCanciones.Find(x=>x.Id==Id);
        }

        public void Modificar(int Id, Cancione cancion)
        {
            BorrarCancion(Id);
            Agregar(cancion);
        }
    }
}

