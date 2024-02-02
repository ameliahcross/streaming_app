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
    }
}

