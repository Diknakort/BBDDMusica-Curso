using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class GeneroesController(IRepositoryGenerico<Genero> repository) : Controller
    {

        // GET: Generoes
        public async Task<IActionResult> Index(string searchString)
        {
            var grupoDContext = await repository.Lista()!;
            {


                var movies = from m in grupoDContext
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Nombre!.Contains(searchString));
                }

                return View("Index",movies.ToList());
            }
        }

        // GET: Generoes/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var genero = await repository.DameUno(id);


            return View("Details",genero);
        }

        // GET: Generoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                await repository.Agregar(genero);

                return RedirectToAction(nameof(Index));
            }

            return View(genero);
        }

        // GET: Generoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var genero = await repository.DameUno(id);


            return View(genero);
        }

        // POST: Generoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.Modificar(genero.Id, genero);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
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

            return View(genero);
        }

        // GET: Generoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var genero = await repository.DameUno(id);



            return View(genero);
        }

        // POST: Generoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await repository.DameUno(id);
            if (genero != null)
            {
                await repository.Borrar(genero);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
            return repository.DameUno(id) != null;
        }
    }
}
