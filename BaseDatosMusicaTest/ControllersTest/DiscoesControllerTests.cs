using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BaseDatosMusica.Tests
{
    [TestClass]
    public class GrupoesControllerTests
    {
        private TestDiscoRepository _discoRepo;
        private TestGrupoRepository _grupoRepo;
        private TestGeneroRepository _generoRepo;
        private DiscoesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _discoRepo = new TestDiscoRepository();
            _grupoRepo = new TestGrupoRepository();
            _generoRepo = new TestGeneroRepository();
            _controller = new DiscoesController(_discoRepo, _grupoRepo, _generoRepo);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfDiscos()
        {
            var result = await _controller.Index(null) as ViewResult;
            var model = result?.Model as IEnumerable<Disco>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task Details_ReturnsViewResult_WithDisco()
        {
            var result = await _controller.Details(1) as ViewResult;
            var model = result?.Model as Disco;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
        }

        [TestMethod]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            var disco = new Disco { Id = 3, Nombre = "Nuevo Disco", GruposId = 1, GeneroId = 1 };
            var result = await _controller.Create(disco) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result?.ActionName);
        }

        [TestMethod]
        public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            var disco = new Disco { Id = 1, Nombre = "Disco Actualizado", GruposId = 1, GeneroId = 1 };
            var result = await _controller.Edit(1, disco) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result?.ActionName);
        }

        [TestMethod]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult()
        {
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result?.ActionName);
        }

        [TestMethod]
        public async Task Delete_ReturnsViewResult_WithDisco()
        {
            var result = await _controller.Delete(1) as ViewResult;
            var model = result?.Model as Disco;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithDisco()
        {
            var result = await _controller.Edit(1) as ViewResult;
            var model = result?.Model as Disco;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
        }

        [TestMethod]
        public async Task Create_ReturnsViewResult_WithSelectLists()
        {
            var result = await _controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private class TestDiscoRepository : IRepositoryGenerico<Disco>
        {
            private readonly List<Disco> discos = new List<Disco>
            {
                new Disco { Id = 1, Nombre = "Disco 1", GruposId = 1, GeneroId = 1 },
                new Disco { Id = 2, Nombre = "Disco 2", GruposId = 2, GeneroId = 2 }
            };

            public Task<bool> Agregar(Disco entidad)
            {
                discos.Add(entidad);
                return Task.FromResult(true);
            }

            public Task<bool> Borrar(Disco entidad)
            {
                discos.Remove(entidad);
                return Task.FromResult(true);
            }

            public Task<Disco> DameUno(int id) => Task.FromResult(discos.FirstOrDefault(d => d.Id == id));

            public Task<List<Disco>> Lista() => Task.FromResult(discos.ToList());

            public Task Modificar(int id, Disco entidad)
            {
                var disco = discos.FirstOrDefault(d => d.Id == id);
                if (disco != null)
                {
                    disco.Nombre = entidad.Nombre;
                    disco.Fecha = entidad.Fecha;
                    disco.GeneroId = entidad.GeneroId;
                    disco.GruposId = entidad.GruposId;
                }
                return Task.CompletedTask;
            }

            public Task<List<Disco>> Filtrar(Expression<Func<Disco, bool>> predicado) =>
                Task.FromResult(discos.AsQueryable().Where(predicado).ToList());
        }

        private class TestGrupoRepository : IRepositoryGenerico<Grupo>
        {
            private readonly List<Grupo> grupos = new List<Grupo>
            {
                new Grupo { Id = 1, Nombre = "Grupo 1" },
                new Grupo { Id = 2, Nombre = "Grupo 2" }
            };

            public Task<bool> Agregar(Grupo entidad)
            {
                grupos.Add(entidad);
                return Task.FromResult(true);
            }

            public Task<bool> Borrar(Grupo entidad)
            {
                grupos.Remove(entidad);
                return Task.FromResult(true);
            }

            public Task<Grupo> DameUno(int id) => Task.FromResult(grupos.FirstOrDefault(g => g.Id == id));

            public Task<List<Grupo>> Lista() => Task.FromResult(grupos.ToList());

            public Task Modificar(int id, Grupo entidad)
            {
                var grupo = grupos.FirstOrDefault(g => g.Id == id);
                if (grupo != null)
                {
                    grupo.Nombre = entidad.Nombre;
                }
                return Task.CompletedTask;
            }

            public Task<List<Grupo>> Filtrar(Expression<Func<Grupo, bool>> predicado) =>
                Task.FromResult(grupos.AsQueryable().Where(predicado).ToList());
        }

        private class TestGeneroRepository : IRepositoryGenerico<Genero>
        {
            private readonly List<Genero> generos = new List<Genero>
            {
                new Genero { Id = 1, Nombre = "Genero 1" },
                new Genero { Id = 2, Nombre = "Genero 2" }
            };

            public Task<bool> Agregar(Genero entidad)
            {
                generos.Add(entidad);
                return Task.FromResult(true);
            }

            public Task<bool> Borrar(Genero entidad)
            {
                generos.Remove(entidad);
                return Task.FromResult(true);
            }

            public Task<Genero> DameUno(int id) => Task.FromResult(generos.FirstOrDefault(g => g.Id == id));

            public Task<List<Genero>> Lista() => Task.FromResult(generos.ToList());

            public Task Modificar(int id, Genero entidad)
            {
                var genero = generos.FirstOrDefault(g => g.Id == id);
                if (genero != null)
                {
                    genero.Nombre = entidad.Nombre;
                }
                return Task.CompletedTask;
            }

            public Task<List<Genero>> Filtrar(Expression<Func<Genero, bool>> predicado) =>
                Task.FromResult(generos.AsQueryable().Where(predicado).ToList());
        }
    }
}