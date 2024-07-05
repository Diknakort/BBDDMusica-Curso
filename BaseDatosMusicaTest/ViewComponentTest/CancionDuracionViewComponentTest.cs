using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Views.Shared.Components;

namespace BaseDatosMusicaTest.ViewComponentTest
{
    [TestClass]
    public class CancionDuracionViewComponentTest
    {
        private CancionDuracionViewComponent component = new();

        [TestMethod]
        public void InvokeAsyncTest()
        {
            var CancionDuracion = component.InvokeAsync(new Cancione()).Result;
            Assert.IsNotNull(CancionDuracion);
        }
    }
}
