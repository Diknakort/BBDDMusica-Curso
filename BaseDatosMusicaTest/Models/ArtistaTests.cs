using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class ArtistaTests
    {
        [TestMethod]
        public void Artista_CanBeInitializedWithDefaultValues()
        {
            var artista = new Artista();
            Assert.AreEqual(0, artista.Id);
            Assert.IsNull(artista.NombreReal);
            Assert.IsNull(artista.NombreArtistico);
            Assert.IsNull(artista.FechaNacimiento);
            Assert.IsNull(artista.RolPrincipal);
            Assert.IsNotNull(artista.Colaboraciones);
            Assert.AreEqual(0, artista.Colaboraciones.Count);
            Assert.IsNull(artista.RolPrincipalNavigation);
        }

        [TestMethod]
        public void Artista_CanSetAndGetProperties()
        {
            var colaboraciones = new List<Colaboracione> { new Colaboracione() };
            var rolPrincipalNavigation = new Role { Id = 1, Nombre = "Cantante" };

            var artista = new Artista
            {
                Id = 1,
                NombreReal = "Donald Trump",
                NombreArtistico = "DT",
                FechaNacimiento = new DateOnly(1990, 1, 1),
                RolPrincipal = 1,
                Colaboraciones = colaboraciones,
                RolPrincipalNavigation = rolPrincipalNavigation
            };

            Assert.AreEqual(1, artista.Id);
            Assert.AreEqual("Donald Trump", artista.NombreReal);
            Assert.AreEqual("DT", artista.NombreArtistico);
            Assert.AreEqual(new DateOnly(1990, 1, 1), artista.FechaNacimiento);
            Assert.AreEqual(1, artista.RolPrincipal);
            CollectionAssert.AreEqual(colaboraciones, (System.Collections.ICollection)artista.Colaboraciones);
            Assert.AreEqual(rolPrincipalNavigation, artista.RolPrincipalNavigation);
        }
    }
}