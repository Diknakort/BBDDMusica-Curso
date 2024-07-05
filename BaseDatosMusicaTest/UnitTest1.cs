using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Specifications;

namespace BaseDatosMusicaTest
{
    [TestClass]
    public class ArtistaCotrollerTest
    {
        [TestMethod]
        public void DiscoTest()
        {
            Disco disco = new Disco();
            disco.Id = 1;
            Cancione cancion = new Cancione();
            cancion.DiscosId = 1;
            DiscoSpecification<Cancione> especificacion = new DiscoSpecification<Cancione>(disco.Id);
            Assert.AreEqual(especificacion.IsValid(cancion), true);
        }

        [TestMethod]
        public void ManagerGrupoTest()
        {
            Manager Manager = new Manager();
            Manager.Id = 1;
            Grupo grupo = new Grupo();
            grupo.ManagersId = 1;
            ManagersGrupoSpecification<Grupo> especification = new ManagersGrupoSpecification<Grupo>(Manager.Id);
            Assert.AreEqual(especification.IsValid(grupo),true);
        }
    }
}