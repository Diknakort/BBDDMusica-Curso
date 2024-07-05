using BaseDatosMusica.Models;
using BaseDatosMusica.Models.MetaData;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class DiscoesController(IRepositoryGenerico<Disco> _context, IRepositoryGenerico<Grupo> Grupos, IRepositoryGenerico<Genero> Generos) : Controller
    {


        // GET: Discoes
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await _context.Lista()!;
            var grupoLista = await Grupos.Lista()!;
            var generoLista = await Generos.Lista()!;
            foreach (var item in grupoDContext)
            {
                item.Grupos = grupoLista.Where(x => x.Id == item.GruposId).FirstOrDefault();
                item.Genero = generoLista.Where(x => x.Id == item.GeneroId).FirstOrDefault();
            }
            {


                //var movies = from m in grupoDContext
                //             select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                //}

                return View("Index", grupoDContext);
            }

        }
        // GET: Discoes
        //public async Task<IActionResult> IndexConsulta(string searchString)
        //{
        //    var grupoDContext = _context.Discos.Include(d => d.Genero).Include(d => d.Grupos);
        //    {
        //        var consulta = _context.Discos;
        //        var elemento = new ConsultasKpop();
        //        var consultaFinal = (elemento as IDiscosQuery).dameDiscos(consulta);
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

        // GET: Discoes/Details/5
        public async Task<IActionResult> Details(int id)
        {


            var disco = await _context.DameUno(id);
            var grupoLista = await Grupos.Lista()!;
            var generoLista = await Generos.Lista()!;
            disco.Grupos = grupoLista.Where(x => x.Id == disco.GruposId).FirstOrDefault();
            disco.Genero = generoLista.Where(x => x.Id == disco.GeneroId).FirstOrDefault();


            return View("Details",disco);
        }

        // GET: Discoes/Create
        public async Task<IActionResult> Create()
        {
            var generos = await Generos.Lista()!;
            var grupo = await Grupos.Lista()!;
            var girasOrdenado = generos.OrderBy(x => x.Nombre);
            var gruposOrdenado = grupo.OrderBy(x => x.Nombre);
            ViewData["GeneroId"] = new SelectList(girasOrdenado!, "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(gruposOrdenado, "Id", "Nombre");
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
                await _context.Agregar(disco);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(await Generos.Lista()!, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(await Grupos.Lista()!, "Id", "Nombre", disco.GruposId);
            return View(disco);
        }

        // GET: Discoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var disco = await _context.DameUno(id);
            var generos = await Generos.Lista()!;
            var grupo = await Grupos.Lista()!;
            var girasOrdenado = generos.OrderBy(x => x.Nombre);
            var gruposOrdenado = grupo.OrderBy(x => x.Nombre);
            ViewData["GeneroId"] = new SelectList(girasOrdenado!, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(gruposOrdenado, "Id", "Nombre", disco.GruposId);
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
                    await _context.Modificar(disco.Id, disco);
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
            ViewData["GeneroId"] = new SelectList(await Generos.Lista()!, "Id", "Nombre", disco.GeneroId);
            ViewData["GruposId"] = new SelectList(await Grupos.Lista()!, "Id", "Nombre", disco.GruposId);
            return View(disco);
        }

        // GET: Discoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var disco = await _context.DameUno(id);


            return View(disco);
        }

        // POST: Discoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disco = await _context.DameUno(id);
            if (disco != null)
            {
                await _context.Borrar(disco);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DiscoExists(int id)
        {
            return _context.DameUno(id) != null;
        }
    }
}
