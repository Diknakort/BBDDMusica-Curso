using BaseDatosMusica.Models;

namespace BaseDatosMusica.Services
{
    public interface ICancionesRepository
    {
        public List<Cancione> dameCanciones();
        public Cancione? DameUna(int Id);
        public bool BorrarCancion(int Id);
        public bool Agregar(Cancione cancion);
        public void Modificar(int Id, Cancione cancion);
    }

}
