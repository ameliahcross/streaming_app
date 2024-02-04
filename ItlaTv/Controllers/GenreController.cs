using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
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
    }
}

