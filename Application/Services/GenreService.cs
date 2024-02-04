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

        // Métodos para Editar y Guardar
        public async Task<SaveGenreViewModel> GetByIdSaveViewModel(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            SaveGenreViewModel genreToSave = new();
            genreToSave.Id = genre.Id;
            genreToSave.Name = genre.Name;
            return genreToSave;
        }

        public async Task Update(SaveGenreViewModel genreToSave)
        {
            Genre genre = new();
            genre.Id = genreToSave.Id;
            genre.Name = genreToSave.Name;

            await _genreRepository.UpdateAsync(genre);
        }

        public async Task Add(SaveGenreViewModel genreToCreate)
        {
            Genre genre = new();
            genre.Id = genreToCreate.Id;
            genre.Name = genreToCreate.Name;

            await _genreRepository.AddAsync(genre);
        }

    }
}

