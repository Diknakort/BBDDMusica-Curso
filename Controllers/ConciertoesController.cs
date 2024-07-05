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
    public class ConciertoesController : Controller
    {
        private readonly GrupoDContext _context;

        public ConciertoesController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Conciertoes
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Conciertos.Include(c => c.Giras).Include(c => c.Grupos);
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
        // GET: Conciertoes
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext = _context.Conciertos.Include(c => c.Giras).Include(c => c.Grupos);
            {
                var consulta = _context.Conciertos;
                var elemento = new ConsultaConciertoscs();
                var consultaFinal = (elemento as IConciertoQuery).dameConciertos(consulta);
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                //var movies = from m in grupoDContext
                //    select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                //}

                return View(consultaFinal);
            }

        }

        // GET: Conciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Giras)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // GET: Conciertoes/Create
        public IActionResult Create()
        {
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre");
            return View();
        }

        // POST: Conciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concierto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", concierto.GirasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", concierto.GruposId);
            return View(concierto);
        }

        // GET: Conciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos.FindAsync(id);
            if (concierto == null)
            {
                return NotFound();
            }
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", concierto.GirasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", concierto.GruposId);
            return View(concierto);
        }

        // POST: Conciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concierto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertoExists(concierto.Id))
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
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", concierto.GirasId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", concierto.GruposId);
            return View(concierto);
        }

        // GET: Conciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await _context.Conciertos
                .Include(c => c.Giras)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // POST: Conciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await _context.Conciertos.FindAsync(id);
            if (concierto != null)
            {
                _context.Conciertos.Remove(concierto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertoExists(int id)
        {
            return _context.Conciertos.Any(e => e.Id == id);
        }
    }
}
