using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class GrupoesController(IRepositoryGenerico<Grupo> context, IRepositoryGenerico<Manager> managers) : Controller
    {

        // GET: Grupoes
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await context.Lista()!;
            var managerLista = await managers.Lista()!;
            foreach (var item in grupoDContext)
            {
                item.Managers = managerLista.Where(x => x.Id == item.ManagersId).FirstOrDefault();
            }
            var movies = from m in grupoDContext
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Nombre!.Contains(searchString));
            }

            return View("Index",movies);
        }

        // GET: Grupoes/IndexConsulta
        //public async Task<IActionResult> IndexConsulta()
        //{
        //    var elemento = new GruposSinDisco(_context);
        //    var consultaFinal = elemento.dameGrupos(_context.Grupos.Include(g => g.Managers));
        //    return View(consultaFinal);
        //}
        // GET: Grupoes
        //public async Task<IActionResult> FiltradoDiscos()
        //{
        //    return View(this._builder.DameDiscosGrupo());
        //}

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int id)
        {


            var grupo = await context.DameUno(id);
            var managerLista = await managers.Lista()!;
            grupo.Managers = managerLista.Where(x => x.Id == grupo.ManagersId).FirstOrDefault();


            return View("Details",grupo);
        }

        // GET: Grupoes/Create
        public async Task<IActionResult> Create()
        {
            var Managers = await managers.Lista()!;
            var ManagersOrdenado = Managers.OrderBy(x => x.Nombre);
            ViewData["ManagersId"] = new SelectList(ManagersOrdenado, "Id", "Nombre");
            return View();
        }

        // POST: Grupoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaCreaccion,ManagersId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                await context.Agregar(grupo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagersId"] = new SelectList(await managers.Lista()!, "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var grupo = await context.DameUno(id);
            var Managers = await managers.Lista()!;
            var ManagersOrdenado = Managers.OrderBy(x => x.Nombre);
            ViewData["ManagersId"] = new SelectList(ManagersOrdenado, "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaCreaccion,ManagersId")] Grupo grupo)
        {
            if (id != grupo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.Modificar(grupo.Id, grupo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagersId"] = new SelectList(await managers.Lista()!, "Id", "Nombre", grupo.ManagersId);
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var grupo = await context.DameUno(id);


            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await context.DameUno(id);
            if (grupo != null)
            {
                await context.Borrar(grupo);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return context.DameUno(id) != null;
        }
    }
}
