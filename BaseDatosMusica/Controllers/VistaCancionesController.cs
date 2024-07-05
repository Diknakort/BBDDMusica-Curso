using BaseDatosMusica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.Controllers
{
    public class VistaCancionesController : Controller
    {
        private readonly GrupoDContext _context;

        public VistaCancionesController(GrupoDContext context)
        {
            _context = context;
        }

        // GET: VistaCanciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.VistaCancione.ToListAsync());
        }

        // GET: VistaCanciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vistaCancione = await _context.VistaCancione
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vistaCancione == null)
            {
                return NotFound();
            }

            return View(vistaCancione);
        }

        //// GET: VistaCanciones/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: VistaCanciones/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Titulo,Nombre,Duracion,Expr1,Id")] VistaCancione vistaCancione)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(vistaCancione);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(vistaCancione);
        //}

        //// GET: VistaCanciones/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vistaCancione = await _context.VistaCancione.FindAsync(id);
        //    if (vistaCancione == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(vistaCancione);
        //}

        //// POST: VistaCanciones/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Titulo,Nombre,Duracion,Expr1,Id")] VistaCancione vistaCancione)
        //{
        //    if (id != vistaCancione.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(vistaCancione);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!VistaCancioneExists(vistaCancione.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(vistaCancione);
        //}

        //// GET: VistaCanciones/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vistaCancione = await _context.VistaCancione
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (vistaCancione == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(vistaCancione);
        //}

        //// POST: VistaCanciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var vistaCancione = await _context.VistaCancione.FindAsync(id);
        //    if (vistaCancione != null)
        //    {
        //        _context.VistaCancione.Remove(vistaCancione);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool VistaCancioneExists(int id)
        //{
        //    return _context.VistaCancione.Any(e => e.Id == id);
        //}
    }
}
