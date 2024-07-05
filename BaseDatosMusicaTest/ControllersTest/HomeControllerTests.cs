using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BaseDatosMusica.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new HomeController(new NullLogger<HomeController>());
        }

        [TestMethod]
        public void Index_Returns_ViewResult()
        {
            var result = _controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Privacy_Returns_ViewResult()
        {
            var result = _controller.Privacy();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Error_Returns_ViewResult_With_ErrorViewModel()
        {
            var activity = new Activity("PruebaActividad");
            activity.Start();
            Activity.Current = activity;

            var result = _controller.Error() as ViewResult;

            Assert.IsNotNull(result);
            var model = result?.Model as ErrorViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(Activity.Current?.Id, model.RequestId);

            activity.Stop();
        }
    }

    public class NullLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}