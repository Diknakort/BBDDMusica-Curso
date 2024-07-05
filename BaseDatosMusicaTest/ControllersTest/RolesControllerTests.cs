using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class RolesControllerTests
{
    private List<Role> roles;
    private RolesController controller;
    private IRepositoryGenerico<Role> repository;

    [TestInitialize]
    public void Initialize()
    {
        roles = new List<Role>
        {
            new Role { Id = 1, Nombre = "Admin" },
            new Role { Id = 2, Nombre = "User" }
        };

        controller = new RolesController(repository);
    }

    [TestMethod]
    public async Task Index_ReturnsAllRoles()
    {
        var result = await controller.Index(null) as ViewResult;
        var model = result.Model as IEnumerable<Role>;

        Assert.AreEqual(2, model.Count());
        Assert.IsTrue(model.Any(m => m.Nombre == "Admin"));
    }

    [TestMethod]
    public async Task Index_SearchFilterWorks()
    {
        var result = await controller.Index("User") as ViewResult;
        var model = result.Model as IEnumerable<Role>;

        Assert.AreEqual(1, model.Count());
        Assert.IsTrue(model.Any(m => m.Nombre == "User"));
    }

    [TestMethod]
    public async Task Details_ReturnsRole()
    {
        var result = await controller.Details(1) as ViewResult;
        var model = result.Model as Role;

        Assert.IsNotNull(model);
        Assert.AreEqual("Admin", model.Nombre);
    }

    [TestMethod]
    public async Task Create_Post_RedirectsToIndex()
    {
        var role = new Role { Id = 3, Nombre = "Editor" };
        var result = await controller.Create(role) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual(3, roles.Count);
    }

    [TestMethod]
    public async Task Edit_Get_ReturnsRole()
    {
        var result = await controller.Edit(1) as ViewResult;
        var model = result.Model as Role;

        Assert.IsNotNull(model);
        Assert.AreEqual("Admin", model.Nombre);
    }

    [TestMethod]
    public async Task Edit_Post_RedirectsToIndex()
    {
        var role = new Role { Id = 1, Nombre = "Administrator" };
        var result = await controller.Edit(1, role) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Administrator", roles.First(r => r.Id == 1).Nombre);
    }

    [TestMethod]
    public async Task Delete_ReturnsRole()
    {
        var result = await controller.Delete(1) as ViewResult;
        var model = result.Model as Role;

        Assert.IsNotNull(model);
        Assert.AreEqual("Admin", model.Nombre);
    }

    [TestMethod]
    public async Task DeleteConfirmed_RedirectsToIndex()
    {
        var result = await controller.DeleteConfirmed(2) as RedirectToActionResult;

        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual(1, roles.Count);
        Assert.IsFalse(roles.Any(r => r.Id == 2));
    }
}