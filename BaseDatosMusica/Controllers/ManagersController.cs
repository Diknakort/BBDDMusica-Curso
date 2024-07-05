using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class ManagersController(IRepositoryGenerico<Manager> repository) : Controller
    {

        // GET: Managers
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await repository.Lista()!;
            {


                var movies = from m in grupoDContext
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                    return View("Index", movies);
                }
                else
                {
                    return View("Index", grupoDContext);
                }
            }
        }

        // GET: Managers
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var grupoDContext = repository.Lista();
        //    {
        //        if (grupoDContext == null)
        //        {
        //            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        //        }

        //        return View(grupoDContext);
        //    }
        //}

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var manager = await repository.DameUno(id);
            return View("Details",manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,FechaNacimiento")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                await repository.Agregar(manager);

                return RedirectToAction(nameof(Index));
            }
            return View("Create",manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var manager = await repository.DameUno(id);

            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaNacimiento")] Manager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.Modificar(id, manager);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.Id))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var manager = await repository.DameUno(id);

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await repository.DameUno(id);
            if (manager != null)
            {
                await repository.Borrar(manager);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return repository.DameUno(id) != null;
        }
    }
}
