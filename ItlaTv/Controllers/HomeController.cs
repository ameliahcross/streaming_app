using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ItlaTv.Models;
using Application.Services;
using Database;
using Application.ViewModels;

namespace ItlaTv.Controllers;

public class HomeController : Controller
{
    private readonly HomeService _homeService;
    private readonly GenreService _genreService;
    private readonly ProducerService _producerService;

    public HomeController(ApplicationContext dbContext)
    {
        _homeService = new(dbContext);
        _genreService = new(dbContext);
        _producerService = new(dbContext);
    }

    public async Task<IActionResult> Index(FilterViewModel filter)
    {
        var model = await _homeService.GetAllViewModelFiltered(filter);
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

