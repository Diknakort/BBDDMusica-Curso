using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BaseDatosMusica.Controllers
{
    public class CancionesController(IRepositoryGenerico<Cancione> repository, IRepositoryGenerico<Disco> repoDiscos) : Controller
    {

        // GET: Canciones
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await repository.Lista()!;
            var discos = await repoDiscos.Lista()!;

            foreach (var item in grupoDContext)
            {
                item.Discos = discos.Where(x => x.Id == item.DiscosId).FirstOrDefault();
            }

            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                var canciones = from m in grupoDContext
                                select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    canciones = canciones.Where(s => s.Titulo!.Contains(searchString));
                    return View("Index", canciones);
                }
                else
                {
                    return View("Index", grupoDContext);
                }
            }

        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cancione = await repository.DameUno(id);
            var disco = await repoDiscos.Lista()!;
            cancione.Discos = disco.Where(x => x.Id == cancione.DiscosId).FirstOrDefault();

            return View("Details", cancione);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            var rol = await repoDiscos.Lista()!;
            var rolOrdenado = rol.OrderBy(x => x.Nombre);
            ViewData["DiscosId"] = new SelectList(rolOrdenado, "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,DiscosId")] Cancione cancione)
        {
            if (ModelState.IsValid)
            {
                await repository.Agregar(cancione);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscosId"] = new SelectList(await repoDiscos.Lista()!, "Id", "Nombre", cancione.DiscosId);
            return View("Create", cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var cancione = await repository.DameUno(id);
            var discos = await repoDiscos.Lista()!;
            cancione.Discos = discos.Where(x => x.Id == cancione.DiscosId).FirstOrDefault();
            var rolOrdenado = discos.OrderBy(x => x.Nombre);
            ViewData["DiscosId"] = new SelectList(rolOrdenado, "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracion,DiscosId")] Cancione cancione)
        {
            if (id != cancione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.Modificar(id, cancione);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancioneExists(cancione.Id))
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
            ViewData["DiscosId"] = new SelectList(await repoDiscos.Lista()!, "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var cancione = await repository.DameUno(id);


            return View(cancione);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancione = await repository.DameUno(id);
            if (cancione != null)
            {
                await repository.Borrar(cancione);
            }


            return RedirectToAction(nameof(Index));
        }

        private bool CancioneExists(int id)
        {
            return repository.DameUno(id) != null;
        }
    }
}
