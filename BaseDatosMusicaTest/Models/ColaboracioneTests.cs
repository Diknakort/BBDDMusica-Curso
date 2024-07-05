using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class ColaboracioneTests
    {
        [TestMethod]
        public void Colaboracione_CanBeInitializedWithDefaultValues()
        {
            var colaboracion = new Colaboracione();
            Assert.AreEqual(0, colaboracion.Id);
            Assert.IsNull(colaboracion.GruposId);
            Assert.IsNull(colaboracion.ArtistasId);
            Assert.IsNull(colaboracion.Grupos);
            Assert.IsNull(colaboracion.Artistas);
        }

        [TestMethod]
        public void Colaboracione_CanSetAndGetProperties()
        {
            var grupo = new Grupo { Id = 1, Nombre = "The Band" };
            var artista = new Artista { Id = 1, NombreArtistico = "Donald Trump" };

            var colaboracion = new Colaboracione
            {
                Id = 1,
                GruposId = 1,
                ArtistasId = 1,
                Grupos = grupo,
                Artistas = artista
            };

            Assert.AreEqual(1, colaboracion.Id);
            Assert.AreEqual(1, colaboracion.GruposId);
            Assert.AreEqual(1, colaboracion.ArtistasId);
            Assert.AreEqual(grupo, colaboracion.Grupos);
            Assert.AreEqual(artista, colaboracion.Artistas);
        }
    }
}