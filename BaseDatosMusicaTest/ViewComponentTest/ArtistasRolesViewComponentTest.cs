using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Views.Shared.Components;
using Microsoft.Extensions.Configuration;

namespace BaseDatosMusicaTest.ViewComponentTest
{
    [TestClass]
    public class ArtistasRolesViewComponentTest
    {
        private ArtistasRolesViewComponent component = new ArtistasRolesViewComponent(coleccion: new EFRepositoryGenerico<Artista>(InitConfiguration()));
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        [TestMethod]
        public void InvokeAsyncTest()
        {
            var ArtistaRol = component.InvokeAsync(1).Result;
            Assert.IsNotNull(ArtistaRol);
        }
    }
}
