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
    public class ConciertoesControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private ConciertoesController controlador = new ConciertoesController (girasRepo: new EFRepositoryGenerico<Gira>(InitConfiguration()), gruposRepo: new EFRepositoryGenerico<Grupo>(InitConfiguration()), context: new EFRepositoryGenerico<Concierto>(InitConfiguration()));
        public EFRepositoryGenerico<Concierto> contexto = new EFRepositoryGenerico<Concierto>(InitConfiguration());


        [TestMethod()]
        public void IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaConcierto = result.ViewData.Model as List<Concierto>;
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
            var concierto = result.ViewData.Model as Concierto;
            Assert.IsNotNull(concierto);
            Assert.AreEqual("noseTuSabras", concierto.Ciudad);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controlador.Create().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
            Assert.IsInstanceOfType(result.ViewData["GirasId"], typeof(SelectList));
            Assert.IsInstanceOfType(result.ViewData["GruposId"], typeof(SelectList));
        }

        [TestMethod()]
        public  async Task  CreateTest1()
        {
            Concierto ConciertoValido = new(){Ciudad = "Zaragoza",FechaHora = new DateTime(2020,2,23),GirasId = 1,GruposId = 1,Precio = 1};
            controlador.Create(ConciertoValido);
            var ConciertoCreado = contexto.Lista().Result.FirstOrDefault(x => x.Ciudad == "Zaragoza");
            Assert.IsNotNull(ConciertoCreado);
            Assert.AreEqual("Zaragoza", ConciertoCreado.Ciudad);
            Assert.AreEqual(1, ConciertoCreado.GirasId);
            controlador.DeleteConfirmed(ConciertoCreado.Id);
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
            Concierto conciertoValido = new() { Ciudad = "Zaragoza", FechaHora = new DateTime(2020, 2, 23), GirasId = 1, GruposId = 1, Precio = 1 };
            controlador.Create(conciertoValido);

            var conciertoCreado = contexto.Lista().Result.FirstOrDefault(x => x.Ciudad == "Zaragoza");
            conciertoCreado.Ciudad = "nose2";
            controlador.Edit(conciertoCreado.Id);

            var conciertoModificado = contexto.Lista().Result.FirstOrDefault(x => x.Ciudad == "nose2");
            Assert.IsNotNull(conciertoModificado);
            Assert.AreEqual("nose2", conciertoModificado.Ciudad);
            await controlador.DeleteConfirmed(conciertoCreado.Id);
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