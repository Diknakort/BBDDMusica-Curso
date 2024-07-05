using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void Role_CanBeInitializedWithDefaultValues()
        {
            var role = new Role();
            Assert.AreEqual(0, role.Id);
            Assert.IsNull(role.Nombre);
            Assert.IsNotNull(role.Artista);
            Assert.AreEqual(0, role.Artista.Count);
        }

        [TestMethod]
        public void Role_CanSetAndGetProperties()
        {
            var artistas = new List<Artista>
            {
                new Artista { Id = 1, NombreArtistico = "Artista 1" },
                new Artista { Id = 2, NombreArtistico = "Artista 2" }
            };

            var role = new Role
            {
                Id = 1,
                Nombre = "Cantante",
                Artista = artistas
            };

            Assert.AreEqual(1, role.Id);
            Assert.AreEqual("Cantante", role.Nombre);
            CollectionAssert.AreEqual(artistas, (System.Collections.ICollection)role.Artista);
        }
    }
}