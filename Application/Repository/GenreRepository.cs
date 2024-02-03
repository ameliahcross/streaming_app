using System;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
	public class GenreRepository
	{
        private readonly ApplicationContext _dbContext;

        public GenreRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _dbContext.Entry(genre).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genre genre)
        {
            _dbContext.Set<Genre>().Remove(genre);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _dbContext.Set<Genre>().ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Genre>().FindAsync(id);
        }
    }
}

