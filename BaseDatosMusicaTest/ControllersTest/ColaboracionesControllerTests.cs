using BaseDatosMusica.Controllers;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BaseDatosMusica.Controllers.Tests
{
    [TestClass()]

    public class ColaboracionesControllerTests
    {
        //public readonly GrupoDContext contexto = new(InitConfiguration());

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        private readonly ColaboracionesController controlador = new(new EFRepositoryGenerico<Colaboracione> (InitConfiguration()), new EFRepositoryGenerico<Artista>(InitConfiguration()), new EFRepositoryGenerico<Grupo>(InitConfiguration()));
        public EFRepositoryGenerico<Colaboracione> contexto = new EFRepositoryGenerico<Colaboracione>(InitConfiguration());



        [TestMethod()]
        public async Task IndexTest()
        {
            var result = controlador.Index("").Result as ViewResult;
            var modelColaboraciones = result?.Model as IEnumerable<Colaboracione>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);

            Assert.IsNotNull(modelColaboraciones);
            Assert.AreEqual(3, modelColaboraciones.Count());




            //Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(ViewResult));
            //Assert.IsNotNull(model);
            //Assert.AreEqual(2, model.Count());
        }

        [TestMethod()]
        public void ColaboracionesControllerTest()
        {
            Assert.Fail();
        }




        [TestMethod()]
        public void DetailsTest()
        {
            var result = controlador.Details(6).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var grupo = result.ViewData.Model as Colaboracione;
            Assert.IsNotNull(grupo);
            Assert.AreEqual(3, grupo.GruposId);
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            var result = controlador.Create().Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.ViewName);


        }

        [TestMethod()]
        public async Task CreateTest1()
        {
            Colaboracione GrupoValido = new Colaboracione() { GruposId = 5, ArtistasId = 45 };
            controlador.Create(GrupoValido);
            var GrupoCreado = contexto.Lista().Result.FirstOrDefault(x => x.GruposId == 5 );
            Assert.IsNotNull(GrupoCreado);
            Assert.AreEqual(5, GrupoCreado.GruposId);
            await controlador.DeleteConfirmed(GrupoCreado.Id);
        }

        [TestMethod()]
        public void EditTest()
        {
            var result = controlador.Edit(1).Result as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task EditTest1()
        {
            Colaboracione colaboraValido = new() { GruposId = 5, ArtistasId = 45 };
            controlador.Create(colaboraValido);

            var colaboraCreado = contexto.Lista().Result.FirstOrDefault(x => x.GruposId == 5 && x.ArtistasId == 46);
            if (colaboraCreado != null)
            {
                colaboraCreado.ArtistasId = 46;
                controlador.Edit(colaboraCreado.Id);

                var colaboraModificado = contexto.Lista().Result.FirstOrDefault(x => x.ArtistasId == 46);
                Assert.IsNotNull(colaboraModificado);
                Assert.AreEqual(46, colaboraModificado.ArtistasId);
                await controlador.DeleteConfirmed(colaboraCreado.Id);
            }
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            int id = (((await controlador.Index("") as ViewResult)).Model as IEnumerable<Colaboracione>)
                .FirstOrDefault(x =>
                    x.ArtistasId.Equals(46) && x.GruposId.Equals(5)).Id;
            var resultado = await controlador.Delete(id) as ViewResult;
            Assert.IsNotNull(resultado);
            Assert.IsNull(resultado.ViewName);
            Assert.IsNotNull(resultado.ViewData.Model);
            var grupoArtista = resultado.ViewData.Model as Colaboracione;
            Assert.IsNotNull(grupoArtista);
            Assert.AreEqual(5, grupoArtista.GruposId);
            Assert.AreEqual(45, grupoArtista.ArtistasId);

            var grupoArtistaEliminar =
                contexto.Lista().Result.FirstOrDefault(x => x.GruposId == 5 && x.ArtistasId == 46);
            Assert.IsNotNull(grupoArtistaEliminar);

            await controlador.DeleteConfirmed(id);

            var artistaGrupoEliminado =
                contexto.Lista().Result.FirstOrDefault(x => x.Id == grupoArtistaEliminar.Id);
            Assert.IsNull(artistaGrupoEliminado);
        }


        [TestMethod()]
        public async Task DeleteConfirmedTest()
        {

            Colaboracione colaboraValido = new() { GruposId = 5, ArtistasId = 46 };
            await controlador.Create(colaboraValido);
            var colaboraCreado = contexto.Lista().Result.FirstOrDefault(x => x.GruposId == 5 && x.ArtistasId == 46);
            await controlador.DeleteConfirmed(colaboraCreado.Id);
            var colaboraBorrado = contexto.Lista().Result.FirstOrDefault(x => x.GruposId == 5 && x.ArtistasId == 46);
            Assert.IsNull(colaboraBorrado);

        }
        [TestMethod()]
        public void ColaboracioneExists_ReturnsTrue_IfColaboracioneExists()
        {
            // Act
            var result = controlador.ColaboracioneExists(1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ColaboracioneExists_ReturnsFalse_IfColaboracioneDoesNotExist()
        {
            // Act
            var result = controlador.ColaboracioneExists(99);

            // Assert
            Assert.IsNull(result);
        }


    }
}
