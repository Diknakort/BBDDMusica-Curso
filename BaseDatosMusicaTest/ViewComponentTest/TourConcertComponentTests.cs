
using System.Linq.Expressions;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.ViewComponents;
using BaseDatosMusica.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BaseDatosMusica.Tests
{
    [TestClass]
    public class TourConcertComponentTests
    {
        [TestMethod]
        public async Task InvokeAsync_ReturnsCorrectViewModel()
        {
            var girasRepo = new TestRepositoryGenerico<Gira>(new List<Gira>
            {
                new Gira { Id = 1, Nombre = "Gira 1" }
            });

            var conciertosRepo = new TestRepositoryGenerico<Concierto>(new List<Concierto>
            {
                new Concierto { Id = 1, GirasId = 1, GruposId = 1 }
            });

            var gruposRepo = new TestRepositoryGenerico<Grupo>(new List<Grupo>
            {
                new Grupo { Id = 1, Nombre = "Grupo 1" }
            });

            var component = new TourConcertComponent(girasRepo, conciertosRepo, gruposRepo);

            var result = await component.InvokeAsync() as ViewViewComponentResult;

            Assert.IsNotNull(result);
            var model = result.ViewData.Model as List<TourWithConcertsViewModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual("Gira 1", model[0].Tour.Nombre);
            Assert.AreEqual(1, model[0].Concerts.Count);
            Assert.AreEqual("Grupo 1", model[0].Concerts[0].Grupos?.Nombre);
        }
    }

    public class TestRepositoryGenerico<T> : IRepositoryGenerico<T> where T : class
    {
        private readonly List<T> _items;

        public TestRepositoryGenerico(List<T> items)
        {
            _items = items;
        }

        public Task<List<T>>? Lista()
        {
            return Task.FromResult(_items);
        }

        public Task<T> DameUno(int id)
        {
            var item = _items.FirstOrDefault(i => (i as dynamic).Id == id);
            return Task.FromResult(item);
        }

        public Task<bool> Agregar(T Object)
        {
            _items.Add(Object);
            return Task.FromResult(true);
        }

        public Task<bool> Borrar(T Object)
        {
            var result = _items.Remove(Object);
            return Task.FromResult(result);
        }

        public Task Modificar(int Id, T Object)
        {
            var index = _items.FindIndex(i => (i as dynamic).Id == Id);
            if (index != -1)
            {
                _items[index] = Object;
            }
            return Task.CompletedTask;
        }

        public Task<List<T>> Filtrar(Expression<Func<T, bool>> predicado)
        {
            var result = _items.AsQueryable().Where(predicado).ToList();
            return Task.FromResult(result);
        }
    }
}