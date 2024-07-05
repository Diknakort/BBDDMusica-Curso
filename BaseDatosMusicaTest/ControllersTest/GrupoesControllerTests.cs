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
    public class GrupoesControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private GrupoesController controlador = new GrupoesController(context: new EFRepositoryGenerico<Grupo>(InitConfiguration()), managers: new EFRepositoryGenerico<Manager>(InitConfiguration()));
        public EFRepositoryGenerico<Manager> contextoManager = new EFRepositoryGenerico<Manager>(InitConfiguration());
        public EFRepositoryGenerico<Grupo> contexto = new EFRepositoryGenerico<Grupo>(InitConfiguration());


        [TestMethod()]
        public void IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaConcierto = result.ViewData.Model as List<Grupo>;
            Assert.IsNotNull(listaConcierto);
            Assert.AreEqual(4, listaConcierto.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = controlador.Details(1).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var grupo = result.ViewData.Model as Grupo;
            Assert.IsNotNull(grupo);
            Assert.AreEqual("FortFiesta", grupo.Nombre);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controlador.Create().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
            Assert.IsInstanceOfType(result.ViewData["ManagersId"], typeof(SelectList));
        }

        [TestMethod()]
        public  async Task  CreateTest1()
        {
            Grupo GrupoValido = new Grupo() { FechaCreaccion = new DateOnly(1900, 12, 11), Nombre = "pepinillo" };
            controlador.Create(GrupoValido);
            var GrupoCreado = contexto.Lista().Result.FirstOrDefault();
            Assert.IsNotNull(GrupoCreado);
            Assert.AreEqual("FortFiesta", GrupoCreado.Nombre);
            await controlador.DeleteConfirmed(GrupoCreado.Id);
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
            Grupo GrupoValido = new Grupo() { FechaCreaccion = new DateOnly(1900, 12, 11), Nombre = "pepinillo" };
            controlador.Create(GrupoValido);

            var GrupoCreado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "pepinillo");
            GrupoCreado.Nombre = "nose2";
            controlador.Edit(GrupoCreado.Id);

            var conciertoModificado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "nose2");
            Assert.IsNotNull(conciertoModificado);
            Assert.AreEqual("nose2", conciertoModificado.Nombre);
            await controlador.DeleteConfirmed(GrupoCreado.Id);
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