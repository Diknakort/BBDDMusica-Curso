using BaseDatosMusica.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseDatosMusica.Services
{
    public class EFRepositoryGenerico<T> : IRepositoryGenerico<T> where T : class
    {
        private readonly IConfiguration _configuration;
        public EFRepositoryGenerico(IConfiguration configuration)
        {
            _context = new(configuration);

        }

        private readonly GrupoDContext _context;

        //private readonly GrupoDContext _context = new();
        public async Task<bool> Agregar(T Object)
        {
            _context.Set<T>().Add(Object);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Borrar(T Object)
        {
            _context.Set<T>().Remove(Object);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> DameUno(int id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        public async Task<List<T>> Filtrar(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task<List<T>> Lista()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Modificar(int Id, T Object)
        {
            _context.Set<T>().Update(Object);
            await _context.SaveChangesAsync();
        }
    }
}

//GrupoDContext