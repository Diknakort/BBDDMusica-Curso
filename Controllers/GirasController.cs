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
    public class GirasController : Controller
    {
        private readonly GrupoDContext _context;

        public GirasController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Giras
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Giras;
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                var movies = from m in grupoDContext
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                }

                return View(await movies.ToListAsync());
            }
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // GET: Giras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Giras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaInicio,Nombre,FechaFinal")] Gira gira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gira);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras.FindAsync(id);
            if (gira == null)
            {
                return NotFound();
            }
            return View(gira);
        }

        // POST: Giras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaInicio,FechaFinal")] Gira gira)
        {
            if (id != gira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiraExists(gira.Id))
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
            return View(gira);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gira = await _context.Giras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gira == null)
            {
                return NotFound();
            }

            return View(gira);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gira = await _context.Giras.FindAsync(id);
            if (gira != null)
            {
                _context.Giras.Remove(gira);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiraExists(int id)
        {
            return _context.Giras.Any(e => e.Id == id);
        }
    }
}
