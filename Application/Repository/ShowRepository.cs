﻿using System;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateAsync(Show show)
        {
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
            //return await _dbContext.Set<Show>().FindAsync(id);	
        }
    }
}