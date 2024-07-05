using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void Manager_CanBeInitializedWithDefaultValues()
        {
            var manager = new Manager();
            Assert.AreEqual(0, manager.Id);
            Assert.IsNull(manager.Nombre);
            Assert.IsNull(manager.FechaNacimiento);
            Assert.IsNotNull(manager.Grupos);
            Assert.AreEqual(0, manager.Grupos.Count);
        }

        [TestMethod]
        public void Manager_CanSetAndGetProperties()
        {
            var grupos = new List<Grupo>
            {
                new Grupo { Id = 1, Nombre = "The Band" },
                new Grupo { Id = 2, Nombre = "Otra Banda" }
            };

            var manager = new Manager
            {
                Id = 1,
                Nombre = "Donald Trump",
                FechaNacimiento = new DateOnly(1980, 1, 1),
                Grupos = grupos
            };

            Assert.AreEqual(1, manager.Id);
            Assert.AreEqual("Donald Trump", manager.Nombre);
            Assert.AreEqual(new DateOnly(1980, 1, 1), manager.FechaNacimiento);
            CollectionAssert.AreEqual(grupos, (System.Collections.ICollection)manager.Grupos);
        }
    }
}