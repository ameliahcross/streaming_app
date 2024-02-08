
using System;
using Database;
using Application.Repository;
using Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using Database;

namespace Application.Services
{
	public class HomeService
	{
        private readonly ShowService _showService;
        private readonly GenreService _genreService;
        private readonly ProducerService _producerService;

        public HomeService(ApplicationContext dbContext)
        {
            _showService = new(dbContext);
            _genreService = new(dbContext);
            _producerService = new(dbContext);
        }

        public async Task<HomeViewModel> GetAllViewModel()
        {
            var shows = await _showService.GetAllViewModel();
            var genres = await _genreService.GetAllViewModel();
            var producers = await _producerService.GetAllViewModel();

            return new HomeViewModel
            {
                Shows = shows,
                Genres = genres,
                Producers = producers
            };
        }

        public async Task<HomeViewModel> GetAllViewModelFiltered(FilterViewModel filters)
        {
            var showViewModels = await _showService.GetFilteredShowsAsync(filters);
            var genres = await _genreService.GetAllViewModel();
            var producers = await _producerService.GetAllViewModel();

            return new HomeViewModel
            {
                Shows = showViewModels,
                Genres = genres,
                Producers = producers,
                SelectedGenreId = filters.SelectedGenreId,
                SelectedProducerId = filters.SelectedProducerId,
                SearchByName = filters.SearchByName
            };
        }
    }
}

