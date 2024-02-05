using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTv.Controllers
{
    public class ShowController : Controller
    {
        private readonly ShowService _showService;

        public ShowController(ApplicationContext dbContext)
        {
            _showService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _showService.GetAllViewModel());
        }

        public async Task<IActionResult> WatchShow(int Id)
        {
            return View(await _showService.GetByIdAsync(Id));
        }
    }
}

