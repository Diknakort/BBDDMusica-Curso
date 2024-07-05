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

namespace BaseDatosMusicaTest.ControllersTest
{
    [TestClass()]
    public class ManagersControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private ManagersController controlador = new ManagersController(repository: new EFRepositoryGenerico<Manager>(InitConfiguration()));
        public EFRepositoryGenerico<Manager> contexto = new EFRepositoryGenerico<Manager>(InitConfiguration());
        [TestMethod()]
        public void IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaConcierto = result.ViewData.Model as List<Manager>;
            Assert.IsNotNull(listaConcierto);
            Assert.AreEqual(5, listaConcierto.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = controlador.Details(1).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var manager = result.ViewData.Model as Manager;
            Assert.IsNotNull(manager);
            Assert.AreEqual("German", manager.Nombre);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = controlador.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);
        }

        [TestMethod()]
        public async Task CreateTest1()
        {
            Manager managerValido = new() { FechaNacimiento = new DateOnly(1999, 2, 2), Nombre = "nose" };
            controlador.Create(managerValido);
            var managerCreado = contexto.Lista().Result.FirstOrDefault(x => x.FechaNacimiento == new DateOnly(1999, 2, 2));
            Assert.IsNotNull(managerCreado);
            Assert.AreEqual("nose", managerCreado.Nombre);
            await controlador.DeleteConfirmed(managerCreado.Id);
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
            Manager managerValido = new() { FechaNacimiento = new DateOnly(1999, 2, 2), Nombre = "Pepinillo" };
            controlador.Create(managerValido);

            var ManagerCreado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "Pepinillo");
            ManagerCreado.Nombre = "nose2";
            controlador.Edit(ManagerCreado.Id);

            var ManagerModificado = contexto.Lista().Result.FirstOrDefault(x => x.Nombre == "nose2");
            Assert.IsNotNull(ManagerModificado);
            Assert.AreEqual("nose2", ManagerModificado.Nombre);
            await controlador.DeleteConfirmed(ManagerCreado.Id);
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
        //[TestMethod()]
        //public void ManagerExists()
        //{
        //    // Act
        //    var result = controlador.ColaboracioneExists(1);

        //    // Assert
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void MaDoesNotExist()
        //{
        //    // Act
        //    var result = controlador.ColaboracioneExists(99);

        //    // Assert
        //    Assert.IsFalse(result);
        //}
    }
}