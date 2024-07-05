using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;

[TestClass]
public class GirasControllerTests
{
    private GirasController controller;
    private FakeRepositoryGenerico<Gira> repository;

    [TestInitialize]
    public void Initialize()
    {
        var giras = new List<Gira>
        {
            new Gira { Id = 1, Nombre = "Gira Summer", FechaInicio = new DateOnly(2024, 6, 1), FechaFinal = new DateOnly(2024, 6, 15) },
            new Gira { Id = 2, Nombre = "Gira Winter", FechaInicio = new DateOnly(2024, 12, 1), FechaFinal = new DateOnly(2024, 12, 15) },
            new Gira { Id = 3, Nombre = "Gira Spring", FechaInicio = new DateOnly(2024, 3, 1), FechaFinal = new DateOnly(2024, 3, 15) }
        };

        repository = new FakeRepositoryGenerico<Gira>(giras);
        controller = new GirasController(repository);
    }

    [TestMethod]
    public async Task Index_ReturnsCompleteList()
    {
        var result = await controller.Index(null) as ViewResult;
        Assert.IsNotNull(result);
        var model = result.Model as List<Gira>;
        Assert.AreEqual(3, model.Count);
    }

    [TestMethod]
    public async Task Index_FiltersByName()
    {
        var result = await controller.Index("Summer") as ViewResult;
        Assert.IsNotNull(result);
        var model = result.Model as List<Gira>;
        Assert.AreEqual(1, model.Count);
        Assert.AreEqual("Gira Summer", model[0].Nombre);
    }

    [TestMethod]
    public async Task Details_ReturnsGira()
    {
        var result = await controller.Details(1) as ViewResult;
        Assert.IsNotNull(result);
        var gira = result.Model as Gira;
        Assert.AreEqual(1, gira.Id);
    }

    [TestMethod]
    public async Task Create_AddsGiraAndRedirects()
    {
        var gira = new Gira { Id = 4, Nombre = "Gira Autumn", FechaInicio = DateOnly.FromDateTime(DateTime.Now), FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddDays(10)) };
        var result = await controller.Create(gira) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual(4, repository.Lista().Result.Count);
    }

    [TestMethod]
    public async Task Edit_UpdatesGiraAndRedirects()
    {
        var gira = new Gira { Id = 1, Nombre = "Updated Gira", FechaInicio = DateOnly.FromDateTime(DateTime.Now), FechaFinal = DateOnly.FromDateTime(DateTime.Now.AddDays(10)) };
        var result = await controller.Edit(1, gira) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        gira = repository.Lista().Result.Find(g => g.Id == 1);
        Assert.AreEqual("Updated Gira", gira.Nombre);
    }

    [TestMethod]
    public async Task Delete_DisplaysCorrectGira()
    {
        var result = await controller.Delete(1) as ViewResult;
        Assert.IsNotNull(result);
        var gira = result.Model as Gira;
        Assert.AreEqual(1, gira.Id);
    }

    [TestMethod]
    public async Task DeleteConfirmed_DeletesGiraAndRedirects()
    {
        var result = await controller.DeleteConfirmed(1) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        var gira = repository.Lista().Result.Find(g => g.Id == 1);
        Assert.IsNull(gira);
    }
}