using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class GirasController(IRepositoryGenerico<Gira> _context) : Controller
    {

        // GET: Giras
        public async Task<IActionResult> Index(string searchString)
        {
            var giras = await _context.Lista()!;

            if (!String.IsNullOrEmpty(searchString))
            {
                giras = giras.Where(s => s.Nombre!.Contains(searchString)).ToList();
            }

            return View("Index",giras);
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int id)
        {


            var gira = await _context.DameUno(id);


            return View("Details",gira);
        }

        // GET: Giras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Giras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaInicio,Nombre,FechaFinal")] Gira gira)
        {
            if (ModelState.IsValid)
            {
                await _context.Agregar(gira);
                return RedirectToAction(nameof(Index));
            }
            return View(gira);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var gira = await _context.DameUno(id);

            return View(gira);
        }

        // POST: Giras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaInicio,Nombre,FechaFinal")] Gira gira)
        {
            if (id != gira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Modificar(gira.Id, gira);
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
        public async Task<IActionResult> Delete(int id)
        {


            var gira = await _context.DameUno(id);


            return View(gira);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gira = await _context.DameUno(id);
            if (gira != null)
            {
                await _context.Borrar(gira);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GiraExists(int id)
        {
            return _context.DameUno(id) != null;
        }
    }
}