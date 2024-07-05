using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;
using BaseDatosMusica.Services;

namespace BaseDatosMusica.Controllers
{
    public class ManagersController : Controller
    {
        private readonly IManagersRepository repository;

        public ManagersController(IManagersRepository repository)
        {
            this.repository = repository;
        }

        // GET: Managers
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var grupoDContext = repository.dameManagers();
        //    {
        //        if (grupoDContext == null)
        //        {
        //            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        //        }

        //        var movies = from m in grupoDContext
        //                     select m;

        //        if (!String.IsNullOrEmpty(searchString))
        //        {
        //            movies = movies.Where(s => s.Nombre!.Contains(searchString));
        //        }

        //        return View(await movies.ToListAsync());
        //    }
        //}

        // GET: Managers
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = repository.dameManagers();
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                return View(grupoDContext.ToList());
            }
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = repository.DameUno(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
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
                repository.Agregar(manager);

                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = repository.DameUno(id);
            if (manager == null)
            {
                return NotFound();
            }
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
                    repository.Modificar(id,manager);
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
            if (id == null)
            {
                return NotFound();
            }

            var manager = repository.DameUno(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = repository.DameUno(id);
            if (manager != null)
            {
                repository.BorrarManager(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return repository.DameUno(id)!=null;
        }
    }
}
