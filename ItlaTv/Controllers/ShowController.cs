using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTv.Controllers
{
    public class ShowController : Controller
    {
        private readonly ShowService _showService;
        private readonly GenreService _genreService;
        private readonly ProducerService _producerService;

        public ShowController(ApplicationContext dbContext)
        {
            _showService = new(dbContext);
            _genreService = new(dbContext);
            _producerService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _showService.GetAllViewModel());
        }

        public async Task<IActionResult> WatchShow(int Id)
        {
            return View(await _showService.GetByIdAsync(Id));
        }

        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetAllViewModel();
            var producers = await _producerService.GetAllViewModel();
            var showViewModel = new SaveShowViewModel
            {
                Genres = genres,
                Producers = producers
            };
            return View("SaveShow", showViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveShowViewModel newShow)
        {
            if (!ModelState.IsValid)
            {
                newShow.Genres = await _genreService.GetAllViewModel();
                newShow.Producers = await _producerService.GetAllViewModel();
                return View("SaveShow", newShow);
            }
            await _showService.Add(newShow);
            return RedirectToRoute(new { controller = "Show", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Retorna por default a la vista "Delete"
            return View(await _showService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _showService.Delete(id);
            return RedirectToRoute(new { controller = "Show", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var showViewModel = await _showService.GetByIdSaveViewModel(id);
            // Debo llamar a method GetAll en el servicio de productoras y géneros
            // para que me devuelva una lista la cual asignaré al viewModel a retornar
            showViewModel.Producers = await _producerService.GetAllViewModel();
            showViewModel.Genres = await _genreService.GetAllViewModel();

            return View("SaveShow", showViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveShowViewModel updatedShowViewModel)
        {
            // Validar que no ingresen el mismo género dos veces
            if (updatedShowViewModel.SecondaryGenreId.HasValue && updatedShowViewModel.SecondaryGenreId == updatedShowViewModel.PrimaryGenreId)
            {
                ModelState.AddModelError("SecondaryGenreId", "Debe seleccionar un género secundario diferente al primario");
            }
            if (!ModelState.IsValid)
            {
                updatedShowViewModel.Genres = await _genreService.GetAllViewModel();
                updatedShowViewModel.Producers = await _producerService.GetAllViewModel();
                return View("Saveshow", updatedShowViewModel);
            }
            await _showService.Update(updatedShowViewModel);
            return RedirectToRoute(new { controller = "Show", action = "Index" });
        }

    }
}
