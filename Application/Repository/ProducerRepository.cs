using System;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
	public class ProducerRepository
	{
        private readonly ApplicationContext _dbContext;

        public ProducerRepository(ApplicationContext dbContext)
		{
            _dbContext = dbContext;
        }

        public async Task AddAsync(Producer producer)
        {
            await _dbContext.Producers.AddAsync(producer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producer producer)
        {
            _dbContext.Entry(producer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Producer producer)
        {
            _dbContext.Set<Producer>().Remove(producer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Producer>> GetAllAsync()
        {
            return await _dbContext.Set<Producer>().ToListAsync();
        }

        public async Task<Producer> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Producer>().FindAsync(id);
        }
    }
}

