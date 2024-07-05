using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class DiscoTests
    {
        [TestMethod]
        public void Disco_CanBeInitializedWithDefaultValues()
        {
            var disco = new Disco();
            Assert.AreEqual(0, disco.Id);
            Assert.IsNull(disco.Nombre);
            Assert.IsNull(disco.Fecha);
            Assert.IsNull(disco.GeneroId);
            Assert.IsNull(disco.GruposId);
            Assert.IsNull(disco.Genero);
            Assert.IsNull(disco.Grupos);
            Assert.IsNotNull(disco.Canciones);
            Assert.AreEqual(0, disco.Canciones.Count);
        }

        [TestMethod]
        public void Disco_CanSetAndGetProperties()
        {
            var genero = new Genero { Id = 1, Nombre = "Rock" };
            var grupo = new Grupo { Id = 1, Nombre = "The Band" };
            var canciones = new List<Cancione>
            {
                new Cancione { Id = 1, Titulo = "Canción 1" },
                new Cancione { Id = 2, Titulo = "Canción 2" }
            };

            var disco = new Disco
            {
                Id = 1,
                Nombre = "Mejor Album",
                Fecha = new DateOnly(2024, 6, 1),
                GeneroId = 1,
                GruposId = 1,
                Genero = genero,
                Grupos = grupo,
                Canciones = canciones
            };

            Assert.AreEqual(1, disco.Id);
            Assert.AreEqual("Mejor Album", disco.Nombre);
            Assert.AreEqual(new DateOnly(2024, 6, 1), disco.Fecha);
            Assert.AreEqual(1, disco.GeneroId);
            Assert.AreEqual(1, disco.GruposId);
            Assert.AreEqual(genero, disco.Genero);
            Assert.AreEqual(grupo, disco.Grupos);
            CollectionAssert.AreEqual(canciones, (System.Collections.ICollection)disco.Canciones);
        }
    }
}