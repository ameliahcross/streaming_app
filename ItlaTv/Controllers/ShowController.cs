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
    }
}
