using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;
using System;
using System.Collections.Generic;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class GiraTests
    {
        [TestMethod]
        public void Gira_CanBeInitializedWithDefaultValues()
        {
            var gira = new Gira();
            Assert.AreEqual(0, gira.Id);
            Assert.IsNull(gira.FechaInicio);
            Assert.IsNull(gira.FechaFinal);
            Assert.IsNull(gira.Nombre);
            Assert.IsNull(gira.Conciertos);
        }
    }
}