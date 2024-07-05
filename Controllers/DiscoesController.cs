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
    public class DiscoesController : Controller
    {
        private readonly GrupoDContext _context;

        public DiscoesController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Discoes
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Discos.Include(d => d.Genero).Include(d => d.Grupos);
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

        // GET: Discoes
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext = _context.Discos.Include(d => d.Genero).Include(d => d.Grupos);
            {
                var consulta = _context.Discos;
                var elemento = new ConsultasKpop();
                var consultaFinal = (elemento as IDiscosQuery).dameDiscos(consulta);
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

        // GET: Discoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .Include(d => d.Genero)
                .Include(d => d.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // GET: Discoes/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre");
            return View();
        }

        // POST: Discoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Fecha,GeneroId,GruposId")] Disco disco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", disco.GruposId);
            return View(disco);
        }

        // GET: Discoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos.FindAsync(id);
            if (disco == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", disco.GruposId);
            return View(disco);
        }

        // POST: Discoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Fecha,GeneroId,GruposId")] Disco disco)
        {
            if (id != disco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscoExists(disco.Id))
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
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", disco.GruposId);
            return View(disco);
        }

        // GET: Discoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disco = await _context.Discos
                .Include(d => d.Genero)
                .Include(d => d.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disco == null)
            {
                return NotFound();
            }

            return View(disco);
        }

        // POST: Discoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.Discos.FindAsync(id);
            if (disco != null)
            {
                _context.Discos.Remove(disco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoExists(int id)
        {
            return _context.Discos.Any(e => e.Id == id);
        }
    }
}
