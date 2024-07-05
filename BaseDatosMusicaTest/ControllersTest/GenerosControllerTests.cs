using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BaseDatosMusica.Controllers.Tests
{
    [TestClass()]
    public class generoControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private GeneroesController controlador = new GeneroesController(repository: new EFRepositoryGenerico<Genero>(InitConfiguration()));
        public EFRepositoryGenerico<Genero> contexto = new EFRepositoryGenerico<Genero>(InitConfiguration());
       

        [TestMethod()]
        public void IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listagenero = result.ViewData.Model as List<Genero>;
            Assert.IsNotNull(listagenero);
            Assert.AreEqual(4, listagenero.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = controlador.Details(1).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var Genero = result.ViewData.Model as Genero;
            Assert.IsNotNull(Genero);
            Assert.AreEqual("HelicopteroApacheDeCombate", Genero.Nombre);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controlador.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
        }

        [TestMethod()]
        public  async Task  CreateTest1()
        {
            Genero GeneroValido = new Genero() {Id = 1,Nombre = "Pepinillo"};
            controlador.Create(GeneroValido);
            var GeneroCreado = contexto.Lista().Result.FirstOrDefault();
            Assert.IsNotNull(GeneroCreado);
            Assert.AreEqual("HelicopteroApacheDeCombate", GeneroCreado.Nombre);
            await controlador.DeleteConfirmed(GeneroCreado.Id);
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
            Genero GeneroValido = new Genero() { Id = 1, Nombre = "Pepinillo" };
            controlador.Create(GeneroValido);

            var GeneroCreado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "Zaragoza");
            GeneroCreado.Nombre = "nose2";
            controlador.Edit(GeneroCreado.Id);

            var conciertoModificado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "nose2");
            Assert.IsNotNull(conciertoModificado);
            Assert.AreEqual("nose2", conciertoModificado.Nombre);
            await controlador.DeleteConfirmed(GeneroCreado.Id);
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