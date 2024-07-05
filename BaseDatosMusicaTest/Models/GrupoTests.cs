using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class GrupoTests
    {
        [TestMethod]
        public void Grupo_CanBeInitializedWithDefaultValues()
        {
            var grupo = new Grupo();
            Assert.AreEqual(0, grupo.Id);
            Assert.IsNull(grupo.Nombre);
            Assert.IsNull(grupo.FechaCreaccion);
            Assert.IsNull(grupo.ManagersId);
            Assert.IsNull(grupo.Managers);
            Assert.IsNotNull(grupo.Colaboraciones);
            Assert.AreEqual(0, grupo.Colaboraciones.Count);
            Assert.IsNotNull(grupo.Conciertos);
            Assert.AreEqual(0, grupo.Conciertos.Count);
            Assert.IsNotNull(grupo.Discos);
            Assert.AreEqual(0, grupo.Discos.Count);
        }

        [TestMethod]
        public void Grupo_CanSetAndGetProperties()
        {
            var colaboraciones = new List<Colaboracione>
            {
                new Colaboracione { Id = 1, GruposId = 1 },
                new Colaboracione { Id = 2, GruposId = 1 }
            };

            var conciertos = new List<Concierto>
            {
                new Concierto { Id = 1, Ciudad = "New York" },
                new Concierto { Id = 2, Ciudad = "Los Angeles" }
            };

            var discos = new List<Disco>
            {
                new Disco { Id = 1, Nombre = "Album 1" },
                new Disco { Id = 2, Nombre = "Album 2" }
            };

            var manager = new Manager { Id = 1, Nombre = "Donald Trump" };

            var grupo = new Grupo
            {
                Id = 1,
                Nombre = "The Band",
                FechaCreaccion = new DateOnly(2000, 1, 1),
                ManagersId = 1,
                Managers = manager,
                Colaboraciones = colaboraciones,
                Conciertos = conciertos,
                Discos = discos
            };

            Assert.AreEqual(1, grupo.Id);
            Assert.AreEqual("The Band", grupo.Nombre);
            Assert.AreEqual(new DateOnly(2000, 1, 1), grupo.FechaCreaccion);
            Assert.AreEqual(1, grupo.ManagersId);
            Assert.AreEqual(manager, grupo.Managers);
            CollectionAssert.AreEqual(colaboraciones, (System.Collections.ICollection)grupo.Colaboraciones);
            CollectionAssert.AreEqual(conciertos, (System.Collections.ICollection)grupo.Conciertos);
            CollectionAssert.AreEqual(discos, (System.Collections.ICollection)grupo.Discos);
        }
    }
}