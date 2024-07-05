using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class ConciertoTests
    {
        [TestMethod]
        public void Concierto_CanBeInitializedWithDefaultValues()
        {
            var concierto = new Concierto();
            Assert.AreEqual(0, concierto.Id);
            Assert.IsNull(concierto.Precio);
            Assert.IsNull(concierto.FechaHora);
            Assert.IsNull(concierto.Ciudad);
            Assert.IsNull(concierto.GruposId);
            Assert.IsNull(concierto.GirasId);
            Assert.IsNull(concierto.Giras);
            Assert.IsNull(concierto.Grupos);
        }

        [TestMethod]
        public void Concierto_CanSetAndGetProperties()
        {
            var gira = new Gira { Id = 1, Nombre = "World Tour" };
            var grupo = new Grupo { Id = 1, Nombre = "The Band" };

            var concierto = new Concierto
            {
                Id = 1,
                Precio = 49.99m,
                FechaHora = new DateTime(2024, 12, 31, 20, 0, 0),
                Ciudad = "New York",
                GruposId = 1,
                GirasId = 1,
                Giras = gira,
                Grupos = grupo
            };

            Assert.AreEqual(1, concierto.Id);
            Assert.AreEqual(49.99m, concierto.Precio);
            Assert.AreEqual(new DateTime(2024, 12, 31, 20, 0, 0), concierto.FechaHora);
            Assert.AreEqual("New York", concierto.Ciudad);
            Assert.AreEqual(1, concierto.GruposId);
            Assert.AreEqual(1, concierto.GirasId);
            Assert.AreEqual(gira, concierto.Giras);
            Assert.AreEqual(grupo, concierto.Grupos);
        }
    }
}