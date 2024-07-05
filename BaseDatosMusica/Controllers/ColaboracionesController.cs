using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class ColaboracionesController(EFRepositoryGenerico<Colaboracione> colabora, EFRepositoryGenerico<Artista> artistaRepositorio, EFRepositoryGenerico<Grupo> grupoRepositorio) : Controller
    {
        //private readonly GrupoDContext _context;

        //public ColaboracionesController(GrupoDContext context)
        //{
        //    _context = context;
        //}

        // GET: Colaboraciones
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await colabora.Lista()!;
            var grupoLista = await artistaRepositorio.Lista()!;
            var generoLista = await grupoRepositorio.Lista()!;
            foreach (var item in grupoDContext)
            {
                item.Artistas = grupoLista.Where(x => x.Id == item.ArtistasId).FirstOrDefault();
                item.Grupos = generoLista.Where(x => x.Id == item.GruposId).FirstOrDefault();
            }
            {
                return View("Index", grupoDContext);
            }
        }




        // GET: Colaboraciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await colabora.DameUno((int)id);
            var grupoLista = await grupoRepositorio.Lista()!;
            var artistaLista = await artistaRepositorio.Lista()!;
            colaboracione.Grupos = grupoLista.Where(x => x.Id == colaboracione.GruposId).FirstOrDefault();
            colaboracione.Artistas = artistaLista.Where(x => x.Id == colaboracione.ArtistasId).FirstOrDefault();
            if (colaboracione == null)
            {
                return NotFound();
            }

            return View("Details", colaboracione);
        }

        // GET: Colaboraciones/Create
        public async Task<IActionResult> Create()
        {
            var colaboracione = await colabora.Lista();
            var grupoLista = await grupoRepositorio.Lista()!;
            var artistaLista = await artistaRepositorio.Lista()!;
            var gruposOrdenado = grupoLista.OrderBy(x => x.Nombre);
            var artistasOrdenado = artistaLista.OrderBy(x => x.NombreArtistico);
            ViewData["ArtistasId"] = new SelectList(gruposOrdenado, "Id", "NombreArtistico");
            ViewData["GruposId"] = new SelectList(artistasOrdenado, "Id", "Nombre");
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
                await colabora.Agregar(new Colaboracione());
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(await artistaRepositorio.Lista(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await grupoRepositorio.Lista(), "Id", "Nombre", colaboracione.GruposId);
            return View("Create", colaboracione);
        }

        // GET: Colaboraciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await colabora.DameUno((int)id);
            if (colaboracione == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(await artistaRepositorio.Lista(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await grupoRepositorio.Lista(), "Id", "Nombre", colaboracione.GruposId);
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
                    await colabora.Modificar(colaboracione.Id, colaboracione);

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
            ViewData["ArtistasId"] = new SelectList(await artistaRepositorio.Lista(), "Id", "NombreArtistico", colaboracione.ArtistasId);
            ViewData["GruposId"] = new SelectList(await grupoRepositorio.Lista(), "Id", "Nombre", colaboracione.GruposId);
            return View(colaboracione);
        }

        // GET: Colaboraciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaboracione = await colabora.DameUno((int)id);
            //.Include(c => c.Artistas)
            //.Include(c => c.Grupos)
            //.FirstOrDefaultAsync(m => m.Id == id);
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
            var colaboracione = await colabora.DameUno((int)id);
            if (colaboracione != null)
            {
                await colabora.Borrar(colaboracione);
            }

            return RedirectToAction(nameof(Index));
        }

        public bool ColaboracioneExists(int id)
        {
            return colabora.DameUno(id) != null;
        }
    }
}
