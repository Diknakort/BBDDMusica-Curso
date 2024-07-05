using BaseDatosMusica.Services;
using System.Linq.Expressions;

public class FakeRepositoryGenerico<T> : IRepositoryGenerico<T> where T : class
{
    private List<T> _list;

    public FakeRepositoryGenerico(List<T> initialData)
    {
        _list = initialData;
    }

    public Task<bool> Agregar(T obj)
    {
        _list.Add(obj);
        return Task.FromResult(true);
    }

    public Task<bool> Borrar(T obj)
    {
        _list.Remove(obj);
        return Task.FromResult(true);
    }

    public Task<List<T>> Lista()
    {
        return Task.FromResult(_list);
    }

    public Task Modificar(int id, T obj)
    {
        int index = _list.FindIndex(item => ((dynamic)item).Id == id);
        if (index != -1)
        {
            _list[index] = obj;
        }
        return Task.CompletedTask;
    }

    public Task<T> DameUno(int id)
    {
        return Task.FromResult(_list.FirstOrDefault(item => ((dynamic)item).Id == id));
    }

    public Task<List<T>> Filtrar(Expression<Func<T, bool>> predicate)
    {
        return Task.FromResult(_list.AsQueryable().Where(predicate).ToList());
    }
}