using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.ViewModels;
using Database;

namespace ItlaTv.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ProducerService _producerService;

        public ProducerController(ApplicationContext dbContext)
        {
            _producerService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _producerService.GetAllViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProducer", await _producerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProducerViewModel saveProducerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", saveProducerViewModel);
            }
            await _producerService.Update(saveProducerViewModel);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

         public async Task<IActionResult> Create()
        {
            return View("SaveProducer", new SaveProducerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProducerViewModel newProducer)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", newProducer);
            }
            await _producerService.Add(newProducer);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            // Retorna por default a la vista "Delete"
            return View(await _producerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _producerService.Delete(id);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }
    }
}

