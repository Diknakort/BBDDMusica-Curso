using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace BaseDatosMusica.Controllers.Tests
{
    [TestClass()]
    public class CancionesControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private CancionesController controlador = new CancionesController(repository: new EFRepositoryGenerico<Cancione>(InitConfiguration()), repoDiscos: new EFRepositoryGenerico<Disco>(InitConfiguration()));
        public EFRepositoryGenerico<Cancione> contexto = new EFRepositoryGenerico<Cancione>(InitConfiguration());
        [TestMethod()]
        public void IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaArtista = result.ViewData.Model as List<Cancione>;
            Assert.IsNotNull(listaArtista);
            Assert.AreEqual(4, listaArtista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = controlador.Details(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var cancion = result.ViewData.Model as Cancione;
            Assert.IsNotNull(cancion);
            Assert.AreEqual("buenas tardes", cancion.Titulo);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controlador.Create().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
            Assert.IsInstanceOfType(result.ViewData["DiscosId"], typeof(SelectList));
        }

        [TestMethod()]
        public async Task CreateTest1()
        {
            Cancione cancionValida = new() { DiscosId = 1, Duracion = new TimeOnly(0, 2, 0), Titulo = "nose1" };
            controlador.Create(cancionValida);
            var cancionCreada = contexto.Lista().Result.FirstOrDefault(x => x.Titulo == "nose1");
            Assert.IsNotNull(cancionCreada);
            Assert.AreEqual("nose1", cancionCreada.Titulo);
            Assert.AreEqual(1, cancionCreada.DiscosId);
             controlador.DeleteConfirmed(cancionCreada.Id);
        }
        [TestMethod()]
        public void EditTest()
        {
            var result = controlador.Edit(2).Result as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task EditTest1()
        {
            Cancione cancionValida = new() { DiscosId = 1, Duracion = new TimeOnly(0, 2, 0), Titulo = "nose1" };
            controlador.Create(cancionValida);
            var cancionCreada = contexto.Lista().Result.FirstOrDefault(x => x.Titulo == "nose1");
            cancionCreada.Titulo = "nose2";
            controlador.Edit(cancionCreada.Id);
            var cancionModificada = contexto.Lista().Result.FirstOrDefault(x => x.Titulo == "nose2");
            Assert.IsNotNull(cancionModificada);
            Assert.AreEqual("nose2", cancionModificada.Titulo);
            await controlador.DeleteConfirmed(cancionCreada.Id);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var result = controlador.Delete(2).Result as ViewResult;
            Assert.IsNotNull(result);
        }

        //[TestMethod()]
        //public void DeleteConfirmedTest()
        //{
        //    Assert.Fail();
        //}
    }
}