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
    public class ArtistasController : Controller
    {
        private readonly GrupoDContext _context;

        public ArtistasController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = _context.Artistas.Include(a => a.RolPrincipalNavigation);
            {
                if (grupoDContext == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }

                var movies = from m in grupoDContext
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.NombreArtistico!.Contains(searchString));
                }

                return View(await movies.ToListAsync());
            }
        }

        // GET: Artistas
        public async Task<IActionResult> IndexConsulta(string searchString)
        {
            var grupoDContext = _context.Artistas.Include(a => a.RolPrincipalNavigation);
            {
                var consulta = _context.Artistas;
                var elemento = new Consulta80();
                var consultaFinal = (elemento as IArtistasQuery).dameArtistas(consulta);
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

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .Include(a => a.RolPrincipalNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            ViewData["RolPrincipal"] = new SelectList(_context.Roles, "Id", "Nombre");
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreReal,NombreArtistico,FechaNacimiento,RolPrincipal")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolPrincipal"] = new SelectList(_context.Role, "Id", "Id", artista.RolPrincipal);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            ViewData["RolPrincipal"] = new SelectList(_context.Roles, "Id", "Id", artista.RolPrincipal);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreReal,NombreArtistico,FechaNacimiento,RolPrincipal")] Artista artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistaExists(artista.Id))
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
            ViewData["RolPrincipal"] = new SelectList(_context.Roles, "Id", "Id", artista.RolPrincipal);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await _context.Artistas
                .Include(a => a.RolPrincipalNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artista == null)
            {
                return NotFound();
            }

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
