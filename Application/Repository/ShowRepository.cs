using System;
using System.Collections.Generic;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Repository
{
    public class ShowRepository
	{
		private readonly ApplicationContext _dbContext;

		public ShowRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task AddAsync(Show show)
		{
			await _dbContext.Shows.AddAsync(show);
			await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Show show, int primaryGenreId, int? secondaryGenreId)
        {
			// Cargo, limpio y repoblar la lista de géneros relacionados a la serie a modificar
            _dbContext.Entry(show).Collection(show => show.Genres).Load();
            show.Genres.Clear();
            // Agrego los nuevos géneros digitados, aún los previamente existentes
            var primaryGenre = await _dbContext.Genres.FindAsync(primaryGenreId);
            show.Genres.Add(primaryGenre);

            if (secondaryGenreId.HasValue && secondaryGenreId.Value != primaryGenreId)
            {
                var secondaryGenre = await _dbContext.Genres.FindAsync(secondaryGenreId.Value);
                if (secondaryGenre != null)
                {
                    show.Genres.Add(secondaryGenre);
                }
            }

            _dbContext.Entry(show).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

		public async Task DeleteAsync(Show show)
		{
			_dbContext.Set<Show>().Remove(show);
			await _dbContext.SaveChangesAsync();
		}

        public async Task<List<Show>> GetAllAsync()
        {
			// Debo incluir los navigation properties a listar ya que son aparte a las propiedades
            return await _dbContext.Shows
		   .Include(producers => producers.Producer)
		   .Include(show => show.Genres)
		   .ToListAsync();
        }

        public async Task<Show> GetByIdAsync(int id)
		{
			return await _dbContext.Shows
                .Include(producers => producers.Producer)
				.Include(show => show.Genres)
                .FirstOrDefaultAsync(show => show.Id == id);
        }

        // Para la relación de muchos a muchos que hay entre Show y Genre,en esta consulta busco y devuelvo una lista
        // de Shows que estén asociados al parámetro (genreId) le pasé al method Delete, de GenreService
        public async Task<List<Show>> GetShowsByGenreIdAsync(int genreId)
        {
            return await _dbContext.Shows // En la entidad Shows
                .Where(show => show.Genres // Por cada Show, busca su lista de géneros
                .Any(genre => genre.Id == genreId)) // De esta lista de géneros, verifica si alguna (.Any tiene
                                                    // el mismo Id que le pasé por parámetro al method con (genreId)
                .Include(show => show.Genres) // A esta lista, le incluyo los géneros relacionados
                .ToListAsync();
        }

        public async Task<List<Show>> GetFilteredShowsAsync(int? genreId, int? producerId, string searchByName)
        {
            var query = await GetAllAsync();

            if (genreId.HasValue)
            {
                query = query.Where(show => show.Genres
                             .Any(genre => genre.Id == genreId))
                             .ToList();
            }
            if (producerId.HasValue)
            {
                query = query.Where(show => show.ProducerId == producerId)
                             .ToList();
            }
            if (!string.IsNullOrEmpty(searchByName))
            {
                query = query.Where(show => show.Name.ToLower()
                              .Contains(searchByName))
                              .ToList();
            }

            return query;
        }

    }
}