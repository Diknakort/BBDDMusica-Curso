using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.Models;

namespace BaseDatosMusica.Controllers
{
    public class CancionesController : Controller
    {
        private readonly GrupoDContext _context;

        public CancionesController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Canciones
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Canciones.Include(c => c.Discos);
           
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
                }

                return View(await canciones.ToListAsync());
            }
           
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Discos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // GET: Canciones/Create
        public IActionResult Create()
        {
            ViewData["DiscosId"] = new SelectList(_context.Discos, "Id", "Nombre");
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
                _context.Add(cancione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscosId"] = new SelectList(_context.Discos, "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione == null)
            {
                return NotFound();
            }
            ViewData["DiscosId"] = new SelectList(_context.Discos, "Id", "Nombre", cancione.DiscosId);
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
                    _context.Update(cancione);
                    await _context.SaveChangesAsync();
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
            ViewData["DiscosId"] = new SelectList(_context.Discos, "Id", "Nombre", cancione.DiscosId);
            return View(cancione);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancione = await _context.Canciones
                .Include(c => c.Discos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancione == null)
            {
                return NotFound();
            }

            return View(cancione);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancione = await _context.Canciones.FindAsync(id);
            if (cancione != null)
            {
                _context.Canciones.Remove(cancione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancioneExists(int id)
        {
            return _context.Canciones.Any(e => e.Id == id);
        }
    }
}
