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
    public class ManagerGruposViewComponentTest
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private ManagerGruposViewComponent component = new ManagerGruposViewComponent(coleccion: new EFRepositoryGenerico<Grupo>(InitConfiguration()));

        [TestMethod]
        public void InvokeAsyncTest()
        {
            var managerGrupo = component.InvokeAsync(1).Result;
            Assert.IsNotNull(managerGrupo);
        }
    }
}
