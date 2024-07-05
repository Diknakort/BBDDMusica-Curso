using System.Linq.Expressions;

namespace BaseDatosMusica.Services
{
    public interface IRepositoryGenerico<T> where T : class
    {
        public Task<List<T>>? Lista();
        public Task<T> DameUno(int id);
        public Task<bool> Agregar(T Object);
        public Task<bool> Borrar(T Object);
        public Task Modificar(int Id, T Object);
        Task<List<T>> Filtrar(Expression<Func<T, bool>> predicado);
    }
}
