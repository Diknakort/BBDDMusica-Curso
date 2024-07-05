using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseDatosMusica.Controllers;

public class ConciertoesController(
    IRepositoryGenerico<Concierto> context,
    IRepositoryGenerico<Gira> girasRepo,
    IRepositoryGenerico<Grupo> gruposRepo)
    : Controller
{
    // GET: Conciertoes
    public async Task<IActionResult> Index(string? searchString)
    {
        var conciertos = await context.Lista()!;
        var girasLista = await girasRepo.Lista()!;
        var gruposLista = await gruposRepo.Lista()!;

        foreach (var item in conciertos)
        {
            item.Giras = girasLista.FirstOrDefault(x => x.Id == item.GirasId);
            item.Grupos = gruposLista.FirstOrDefault(x => x.Id == item.GruposId);
        }

        var filteredConciertos = conciertos.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            filteredConciertos = filteredConciertos.Where(m => m.Grupos!.Nombre!.Contains(searchString));
        }

        return View("Index",filteredConciertos.ToList());
    }

    // GET: Conciertoes/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var concierto = await context.DameUno(id);

        if (concierto == null) return NotFound();

        concierto.Giras = (await girasRepo.Lista()!)?.FirstOrDefault(x => x.Id == concierto.GirasId);
        concierto.Grupos = (await gruposRepo.Lista()!)?.FirstOrDefault(x => x.Id == concierto.GruposId);

        return View("Details",concierto);
    }

    // GET: Conciertoes/Create
    public async Task<IActionResult> Create()
    {
        ViewData["GirasId"] = new SelectList(await girasRepo.Lista()!, "Id", "Nombre");
        ViewData["GruposId"] = new SelectList(await gruposRepo.Lista()!, "Id", "Nombre");
        return View();
    }

    // POST: Conciertoes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
    {
        if (ModelState.IsValid)
        {
            await context.Agregar(concierto);
            return RedirectToAction(nameof(Index));
        }

        ViewData["GirasId"] = new SelectList(await girasRepo.Lista()!, "Id", "Nombre", concierto.GirasId);
        ViewData["GruposId"] = new SelectList(await gruposRepo.Lista()!, "Id", "Nombre", concierto.GruposId);
        return View("Create",concierto);
    }

    // GET: Conciertoes/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var concierto = await context.DameUno(id);
        if (concierto == null) return NotFound();

        ViewData["GirasId"] = new SelectList(await girasRepo.Lista()!, "Id", "Nombre", concierto.GirasId);
        ViewData["GruposId"] = new SelectList(await gruposRepo.Lista()!, "Id", "Nombre", concierto.GruposId);
        return View(concierto);
    }

    // POST: Conciertoes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Precio,FechaHora,Ciudad,GruposId,GirasId")] Concierto concierto)
    {
        if (id != concierto.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await context.Modificar(concierto.Id, concierto);
            }
            catch (Exception)
            {
                if (!await ConciertoExists(concierto.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["GirasId"] = new SelectList(await girasRepo.Lista()!, "Id", "Nombre", concierto.GirasId);
        ViewData["GruposId"] = new SelectList(await gruposRepo.Lista()!, "Id", "Nombre", concierto.GruposId);
        return View(concierto);
    }

    // GET: Conciertoes/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var concierto = await context.DameUno(id);

        if (concierto == null) return NotFound();

        return View(concierto);
    }

    // POST: Conciertoes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var concierto = await context.DameUno(id);
        if (concierto != null)
        {
            await context.Borrar(concierto);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ConciertoExists(int id)
    {
        return await context.DameUno(id) != null;
    }
}