using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class CancioneTests
    {
        [TestMethod]
        public void Cancione_CanBeInitializedWithDefaultValues()
        {
            var cancion = new Cancione();
            Assert.AreEqual(0, cancion.Id);
            Assert.IsNull(cancion.Titulo);
            Assert.IsNull(cancion.Duracion);
            Assert.IsNull(cancion.DiscosId);
            Assert.IsNull(cancion.Discos);
        }

        [TestMethod]
        public void Cancione_CanSetAndGetProperties()
        {
            var disco = new Disco { Id = 1, Nombre = "Mejores éxitos" };

            var cancion = new Cancione
            {
                Id = 1,
                Titulo = "Song One",
                Duracion = new TimeOnly(0, 3, 45),
                DiscosId = 1,
                Discos = disco
            };

            Assert.AreEqual(1, cancion.Id);
            Assert.AreEqual("Song One", cancion.Titulo);
            Assert.AreEqual(new TimeOnly(0, 3, 45), cancion.Duracion);
            Assert.AreEqual(1, cancion.DiscosId);
            Assert.AreEqual(disco, cancion.Discos);
            Assert.AreEqual("Mejores éxitos", cancion.Discos?.Nombre);
        }
    }
}