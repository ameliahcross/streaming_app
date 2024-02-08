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
        private readonly ShowRepository _showRepository;

        public GenreService(ApplicationContext dbContext)
        {
            _genreRepository = new(dbContext);
            _showRepository = new(dbContext);
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

        // Esto me devuelve un booleano para saber qué mostrar en la vista dependiendo
        // si el género a eliminar es o no único en un show
        public async Task<bool> Delete(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return false;
            }

            // Aquí obtengo la lista de series vinculadas con el Id del género que se quiere eliminar.
            var showsWithThisGenreId = await _showRepository.GetShowsByGenreIdAsync(id);

            // Devuelvo TRUE si alguna serie de mi lista filtrada, tiene este género como su ÚNICO género.
            bool hasShowsWithSingleGenre = showsWithThisGenreId.Any(show => show.Genres.Count == 1);

            // Si alguna serie tiene este género como su único género, no permitirá eliminar el género
            // en el controlador y aquí simplemente devuelvo true y NO ejecuto el method "DeleteAsync" del repositorio
            if (hasShowsWithSingleGenre)
            {
                return true;
            }

            // Si el género existe pero no hay series que lo tengan como único género,
            // permitirá eliminar el género
            await _genreRepository.DeleteAsync(genre);
            return false;
        }

    }
}

