using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseDatosMusica.Models;

namespace BaseDatosMusica.Tests.Models
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void ShowRequestId_ShouldReturnTrue_WhenRequestIdIsNotNullOrEmpty()
        {
            var model = new ErrorViewModel { RequestId = "12345" };
            Assert.IsTrue(model.ShowRequestId);
        }

        [TestMethod]
        public void ShowRequestId_ShouldReturnFalse_WhenRequestIdIsNull()
        {
            var model = new ErrorViewModel { RequestId = null };
            Assert.IsFalse(model.ShowRequestId);
        }

        [TestMethod]
        public void ShowRequestId_ShouldReturnFalse_WhenRequestIdIsEmpty()
        {
            var model = new ErrorViewModel { RequestId = string.Empty };
            Assert.IsFalse(model.ShowRequestId);
        }
    }
}