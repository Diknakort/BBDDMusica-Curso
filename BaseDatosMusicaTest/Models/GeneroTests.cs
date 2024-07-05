using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class GeneroTests
    {
        [TestMethod]
        public void Genero_CanBeInitializedWithDefaultValues()
        {
            var genero = new Genero();
            Assert.AreEqual(0, genero.Id);
            Assert.IsNull(genero.Nombre);
            Assert.IsNotNull(genero.Discos);
            Assert.AreEqual(0, genero.Discos.Count);
        }

        [TestMethod]
        public void Genero_CanSetAndGetProperties()
        {
            var discos = new List<Disco>
            {
                new Disco { Id = 1, Nombre = "Album 1" },
                new Disco { Id = 2, Nombre = "Album 2" }
            };

            var genero = new Genero
            {
                Id = 1,
                Nombre = "Rock",
                Discos = discos
            };

            Assert.AreEqual(1, genero.Id);
            Assert.AreEqual("Rock", genero.Nombre);
            CollectionAssert.AreEqual(discos, (System.Collections.ICollection)genero.Discos);
        }
    }
}