using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ItlaTv.Models;
using Application.Services;
using Database;

namespace ItlaTv.Controllers;

public class HomeController : Controller
{
    private readonly HomeService _homeService;

    public HomeController(ApplicationContext dbContext)
    {
        _homeService = new(dbContext);
    }

    public async Task<IActionResult> Index()
    {
        return View(await _homeService.GetAllViewModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

