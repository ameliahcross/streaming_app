using System;
using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;

        public GenreService(ApplicationContext dbContext)
        {
            _genreRepository = new(dbContext);
        }

        public async Task<List<GenreViewModel>> GetAllViewModel()
        {
            var genresList = await _genreRepository.GetAllAsync();

            return genresList.Select(genre => new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            }).ToList();

        }


    }
}

