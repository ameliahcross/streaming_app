using System;
using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;

namespace Application.Services
{
	public class ShowService
	{
		private readonly ShowRepository _showRepository;
        private readonly GenreRepository _genreRepository;

        public ShowService(ApplicationContext dbContext)
		{
			_showRepository = new(dbContext);
            _genreRepository = new(dbContext);
        }

        public async Task<List<ShowViewModel>> GetAllViewModel()
        {
            var showList = await _showRepository.GetAllAsync();

            return showList.Select(show => new ShowViewModel
            {
                Id = show.Id,
                Name = show.Name,
                ImageUrl = show.ImageUrl,
                VideoUrl = show.VideoUrl,
                ProducerName = show.Producer?.Name,
                PrimaryGenreName = show.Genres.FirstOrDefault()?.Name,
                SecondaryGenreName = show.Genres.Skip(1).FirstOrDefault()?.Name,

            }).ToList();
        }

        public async Task<ShowViewModel> GetByIdAsync(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            ShowViewModel showViewModel = new();
            showViewModel.Id = show.Id;
            showViewModel.Name = show.Name;
            showViewModel.VideoUrl = show.VideoUrl;
            return showViewModel;
        }

        public async Task Add(SaveShowViewModel showToCreate)
        {
            Show show = new()
            {
                Name = showToCreate.Name,
                ImageUrl = showToCreate.ImageUrl,
                VideoUrl = showToCreate.VideoUrl,
                ProducerId = showToCreate.ProducerId,
                Genres = new List<Genre>()
            };

            var primaryGenre = await _genreRepository.GetByIdAsync(showToCreate.PrimaryGenreId);

            if (showToCreate.SecondaryGenreId.HasValue)
            {
                var secondaryGenre = await _genreRepository.GetByIdAsync(showToCreate.SecondaryGenreId.Value);
                if (secondaryGenre != null)
                {
                    show.Genres.Add(secondaryGenre);
                }
            }
            await _showRepository.AddAsync(show);
        }

    }
}

