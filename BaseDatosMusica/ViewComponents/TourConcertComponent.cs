using BaseDatosMusica.Models;
using BaseDatosMusica.ViewModels;
using BaseDatosMusica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseDatosMusica.ViewComponents;

public class TourConcertComponent : ViewComponent
{
    private readonly IRepositoryGenerico<Gira> _girasRepo;
    private readonly IRepositoryGenerico<Concierto> _conciertosRepo;
    private readonly IRepositoryGenerico<Grupo> _gruposRepo;

    public TourConcertComponent(IRepositoryGenerico<Gira> girasRepo, IRepositoryGenerico<Concierto> conciertosRepo, IRepositoryGenerico<Grupo> gruposRepo)
    {
        _girasRepo = girasRepo;
        _conciertosRepo = conciertosRepo;
        _gruposRepo = gruposRepo;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var girasList = await _girasRepo.Lista()!;
        var conciertosList = await _conciertosRepo.Lista()!;
        var gruposList = await _gruposRepo.Lista()!;

        var tourConcertViewModel = new List<TourWithConcertsViewModel>();

        foreach (var gira in girasList)
        {
            var conciertos = conciertosList.Where(c => c.GirasId == gira.Id).ToList();

            foreach (var concierto in conciertos)
            {
                concierto.Grupos = gruposList.FirstOrDefault(g => g.Id == concierto.GruposId);
            }

            tourConcertViewModel.Add(new TourWithConcertsViewModel
            {
                Tour = gira,
                Concerts = conciertos
            });
        }

        return View(tourConcertViewModel);
    }
}