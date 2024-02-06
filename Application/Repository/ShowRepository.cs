using System;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
   // Clase para operaciones de acceso a datos directas, sin lógica de negocio
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
			// Se deben incluir los navigation properties a listar
			// ya que son aparte a las propiedades
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

        // Para la relación de muchos a muchos que hay entre Show y Genre,
        // aqui busco y devuelvo una lista de Shows que estén asociados al
        // género que se le pase al method por parámetro (genreId)
        public async Task<List<Show>> GetShowsByGenreIdAsync(int genreId)
        {
            return await _dbContext.Shows
                .Where(show => show.Genres.Any(genre => genre.Id == genreId))
                .Include(show => show.Genres)
                .ToListAsync();
        }
    }
}