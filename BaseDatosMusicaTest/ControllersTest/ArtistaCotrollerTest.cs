using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class ArtistaControllerTest
{
    private ArtistasController controller;
    private List<Artista> artistasData;
    private List<Role> rolesData;

    [TestInitialize]
    public void Setup()
    {
        artistasData = new List<Artista>
        {
            new Artista { Id = 1, NombreArtistico = "Artista 1", NombreReal = "Real 1", FechaNacimiento = new DateOnly(2001, 1, 1), RolPrincipal = 1 },
            new Artista { Id = 2, NombreArtistico = "Artista 2", NombreReal = "Real 2", FechaNacimiento = new DateOnly(2002, 2, 1), RolPrincipal = 2 },
            new Artista { Id = 3, NombreArtistico = "Artista 3", NombreReal = "Real 3", FechaNacimiento = new DateOnly(2003, 3, 1), RolPrincipal = 3 },
            new Artista { Id = 4, NombreArtistico = "Artista 4", NombreReal = "Real 4", FechaNacimiento = new DateOnly(2004, 4, 1), RolPrincipal = 4 },
            new Artista { Id = 5, NombreArtistico = "Artista 5", NombreReal = "Real 5", FechaNacimiento = new DateOnly(2005, 5, 1), RolPrincipal = 5 },
            new Artista { Id = 6, NombreArtistico = "Artista 6", NombreReal = "Real 6", FechaNacimiento = new DateOnly(2006, 6, 1), RolPrincipal = 6 },
            new Artista { Id = 7, NombreArtistico = "Artista 7", NombreReal = "Real 7", FechaNacimiento = new DateOnly(2007, 7, 1), RolPrincipal = 7 }
        };

        rolesData = new List<Role>
        {
            new Role { Id = 1, Nombre = "Role 1" },
            new Role { Id = 2, Nombre = "Role 2" },
            new Role { Id = 3, Nombre = "Role 3" },
            new Role { Id = 4, Nombre = "Role 4" },
            new Role { Id = 5, Nombre = "Role 5" },
            new Role { Id = 6, Nombre = "Role 6" },
            new Role { Id = 7, Nombre = "Role 7" }
        };

        var fakeArtistaRepo = new FakeRepositoryGenerico<Artista>(artistasData);
        var fakeRoleRepo = new FakeRepositoryGenerico<Role>(rolesData);

        controller = new ArtistasController(_context: fakeArtistaRepo, roles: fakeRoleRepo);
    }

    [TestMethod]
    public async Task IndexTest()
    {
        var result = await controller.Index("") as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(List<Artista>));
        var model = result.Model as List<Artista>;
        Assert.AreEqual(7, model.Count); // Editar según la necesidad
    }

    [TestMethod]
    public async Task DetailsTest()
    {
        var result = await controller.Details(2) as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(Artista));
        var model = result.Model as Artista;
        Assert.AreEqual("Artista 2", model.NombreArtistico);
    }

    [TestMethod]
    public async Task CreateTest_Post()
    {
        var nuevoArtista = new Artista { Id = 8, NombreArtistico = "Nuevo Artista", NombreReal = "Nuevo Real", FechaNacimiento = new DateOnly(2008, 8, 1), RolPrincipal = 8 };

        var result = await controller.Create(nuevoArtista) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual(8, artistasData.Count);
        Assert.IsTrue(artistasData.Any(a => a.NombreArtistico == "Nuevo Artista"));
    }

    [TestMethod]
    public async Task EditTest_Post()
    {
        var artista = artistasData.First(a => a.Id == 3);
        artista.NombreReal = "Edited Real Name";

        var result = await controller.Edit(3, artista) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Edited Real Name", artistasData.First(a => a.Id == 3).NombreReal);
    }

    [TestMethod]
    public async Task DeleteTest_Post()
    {
        var result = await controller.DeleteConfirmed(2) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.IsFalse(artistasData.Any(a => a.Id == 2));
    }
}