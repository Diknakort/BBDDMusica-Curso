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
    public class ColaboracionesController : Controller
    {
        private readonly GrupoDContext _context;

        public ColaboracionesController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Colaboraciones
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Colaboraciones.Include(c => c.Artistas).Include(c => c.Grupos);
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                var movies = from m in grupoDContext
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Grupos.Nombre!.Contains(searchString));
                }

                return View(await movies.ToListAsync());
            }
           
        }

        // GET: Colaboraciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.Colaboraciones
                .Include(c => c.Artistas)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colaboracione == null)
            {
                return NotFound();
            }

            return View(colaboracione);
        }

        // GET: Colaboraciones/Create
        public IActionResult Create()
        {
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "NombreArtistico");
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre");
            return View();
        }

        // POST: Colaboraciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colaboracione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // GET: Colaboraciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.Colaboraciones.FindAsync(id);
            if (colaboracione == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // POST: Colaboraciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GruposId,ArtistasId")] Colaboracione colaboracione)
        {
            if (id != colaboracione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colaboracione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColaboracioneExists(colaboracione.Id))
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
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // GET: Colaboraciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await _context.Colaboraciones
                .Include(c => c.Artistas)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colaboracione == null)
            {
                return NotFound();
            }

            return View(colaboracione);
        }

        // POST: Colaboraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colaboracione = await _context.Colaboraciones.FindAsync(id);
            if (colaboracione != null)
            {
                _context.Colaboraciones.Remove(colaboracione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColaboracioneExists(int id)
        {
            return _context.Colaboraciones.Any(e => e.Id == id);
        }
    }
}
