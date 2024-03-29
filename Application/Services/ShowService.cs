﻿using System;
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
        private readonly ProducerRepository _producerRepository;

        public ShowService(ApplicationContext dbContext)
		{
			_showRepository = new(dbContext);
            _genreRepository = new(dbContext);
            _producerRepository = new(dbContext);
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

        public async Task<SaveShowViewModel> GetByIdSaveViewModel(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);

            SaveShowViewModel showToSave = new();
            showToSave.Id = show.Id;
            showToSave.Name = show.Name;
            showToSave.ImageUrl = show.ImageUrl;
            showToSave.VideoUrl = show.VideoUrl;
            showToSave.ProducerId = show.ProducerId;
            showToSave.PrimaryGenreId = show.Genres.FirstOrDefault().Id;
            showToSave.SecondaryGenreId = show.Genres.Skip(1).FirstOrDefault()?.Id;

            return showToSave;
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
            show.Genres.Add(primaryGenre);
            await _showRepository.AddAsync(show);
        }

        public async Task Delete(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            await _showRepository.DeleteAsync(show);
        }

        public async Task Update(SaveShowViewModel showToSave)
        {
            var show = await _showRepository.GetByIdAsync(showToSave.Id);

            show.Name = showToSave.Name;
            show.ImageUrl = showToSave.ImageUrl;
            show.VideoUrl = showToSave.VideoUrl;
            show.ProducerId = showToSave.ProducerId;

            // Además del show con sus propiedades básicas, le debo pasar al method, los ID de los dos géneros
            // que el show a editar tiene, para que el servicio los reciba y pueda actualizarlos.
            await _showRepository.UpdateAsync(show, showToSave.PrimaryGenreId, showToSave.SecondaryGenreId);
        }

        public async Task<IEnumerable<ShowViewModel>> GetFilteredShowsAsync(FilterViewModel filters)
        {
            var shows = await _showRepository.GetFilteredShowsAsync(filters.SelectedGenreId, filters.SelectedProducerId, filters.SearchByName);

            return shows.Select(show => new ShowViewModel
            {
                Name = show.Name,
                Id = show.Id,
                ImageUrl = show.ImageUrl,
                VideoUrl = show.VideoUrl,
                ProducerId = show.ProducerId,
                PrimaryGenreName = show.Genres.FirstOrDefault()?.Name,
                SecondaryGenreName = show.Genres.Skip(1).FirstOrDefault()?.Name,
                ProducerName = show.Producer?.Name
            });
        }


    }
}

