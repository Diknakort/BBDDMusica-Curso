using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class ArtistasController(IRepositoryGenerico<Artista> _context, IRepositoryGenerico<Role> roles) : Controller
    {
        // GET: Artistas
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await _context.Lista()!;
            var rol = await roles.Lista()!;
            foreach (var item in grupoDContext)
            {
                item.RolPrincipalNavigation = rol.Where(x => x.Id == item.RolPrincipal).FirstOrDefault();
            }

            {
                var movies = from m in grupoDContext select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.NombreArtistico!.Contains(searchString));
                    return View("Index", movies);
                }
                else
                {
                    return View("Index", grupoDContext);
                }

            }
        }

        // GET: Artistas
        //public async Task<IActionResult> IndexConsulta(string searchString)
        //{
        //    var grupoDContext = _context.Artistas.Include(a => a.RolPrincipalNavigation);
        //    {
        //        var consulta = _context.Artistas;
        //        var elemento = new Consulta80();
        //        var consultaFinal = (elemento as IArtistasQuery).dameArtistas(consulta);
        //        if (grupoDContext == null)
        //        {
        //            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        //        }

        //        //var movies = from m in grupoDContext
        //        //    select m;

        //        //if (!String.IsNullOrEmpty(searchString))
        //        //{
        //        //    movies = movies.Where(s => s.Nombre!.Contains(searchString));
        //        //}

        //        return View(consultaFinal);
        //    }
        //}

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var artista = await _context.DameUno(id);
            var rol = await roles.Lista()!;
            artista.RolPrincipalNavigation = rol.Where(x => x.Id == artista.RolPrincipal).FirstOrDefault();

            return View("Details", artista);
        }

        // GET: Artistas/Create
        public async Task<IActionResult> Create()
        {
            var rol = await roles.Lista()!;
            var rolOrdenado = rol.OrderBy(x => x.Nombre);
            ViewData["RolPrincipal"] = new SelectList(rolOrdenado, "Id", "Nombre");
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
                await _context.Agregar(artista);
                return RedirectToAction(nameof(Index));
            }

            var rol = await roles.Lista()!;
            var rolOrdenado = rol.OrderBy(x => x.Nombre);
            ViewData["RolPrincipal"] = new SelectList(rolOrdenado, "Id", "Nombre", artista.RolPrincipal);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var artista = await _context.DameUno(id);
            var rol = await roles.Lista()!;
            artista.RolPrincipalNavigation = rol.Where(x => x.Id == artista.RolPrincipal).FirstOrDefault();
            ViewData["RolPrincipal"] = new SelectList(await roles.Lista()!, "Id", "Nombre", artista.RolPrincipal);
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
                    await _context.Modificar(artista.Id, artista);
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
            ViewData["RolPrincipal"] = new SelectList(await roles.Lista()!, "Id", "Nombre", artista.RolPrincipal);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var artista = await _context.DameUno(id);

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _context.DameUno(id);
            if (artista != null)
            {
                await _context.Borrar(artista);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistaExists(int id)
        {
            return _context.DameUno(id) != null;
        }
    }
}
