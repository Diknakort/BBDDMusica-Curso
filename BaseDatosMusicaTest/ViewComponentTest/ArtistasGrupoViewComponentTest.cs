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
    public class ArtistasGrupoViewComponentTest
    {
        private ArtistasGrupoViewComponent component = new ArtistasGrupoViewComponent(Artistas: new EFRepositoryGenerico<Artista>(InitConfiguration()), Colabs: new EFRepositoryGenerico<Colaboracione>(InitConfiguration()), Roles: new EFRepositoryGenerico<Role>(InitConfiguration()));
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        public EFRepositoryGenerico<Artista> contexto = new EFRepositoryGenerico<Artista>(InitConfiguration());
        [TestMethod]
        public void InvokeAsyncTest()
        {
            var ArtistaGrupo = component.InvokeAsync(1).Result;
            Assert.IsNotNull(ArtistaGrupo);
        }
    }
}
