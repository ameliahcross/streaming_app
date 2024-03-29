﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.ViewModels;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTv.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;

        public GenreController(ApplicationContext dbContext)
        {
            _genreService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetAllViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveGenre", await _genreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveGenreViewModel saveGenreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", saveGenreViewModel);
            }
            await _genreService.Update(saveGenreViewModel);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> Create()
        {
            return View("SaveGenre", new SaveGenreViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveGenreViewModel newGenre)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", newGenre);
            }
            await _genreService.Add(newGenre);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Retorna por default a la vista "Delete"
            return View(await _genreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int Id)
        {
            var genre = await _genreService.GetByIdSaveViewModel(Id);
            var answer = await _genreService.Delete(Id);

            if (answer == true)
            {
                TempData["DeleteGenreWarning"] = $" * No se puede eliminar el género: '{genre.Name}' porque hay series que dependen exclusivamente de él.";
                return RedirectToAction("Delete", new { id = Id });
            }

            await _genreService.Delete(Id);
            return RedirectToRoute(new { controller = "Genre", action = "Index" });
        }
            
        
    }
}

