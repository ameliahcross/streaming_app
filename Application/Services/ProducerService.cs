using System;
using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;

namespace Application.Services
{
	public class ProducerService
	{
		private readonly ProducerRepository _producerRepository;

		public ProducerService(ApplicationContext dbContext)
		{
			_producerRepository = new(dbContext);
        }

        public async Task<List<ProducerViewModel>> GetAllViewModel()
        {
            var producersList = await _producerRepository.GetAllAsync();

            return producersList.Select(producer => new ProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            }).ToList();
        }

        public async Task<SaveProducerViewModel> GetByIdSaveViewModel(int id)
        {
            var producer = await _producerRepository.GetByIdAsync(id);

            SaveProducerViewModel producerToSave = new();
            producerToSave.Id = producer.Id;
            producerToSave.Name = producer.Name;
            return producerToSave;
        }

        public async Task Update(SaveProducerViewModel producerToSave)
        {
            Producer producer = new();
            producer.Id = producerToSave.Id;
            producer.Name = producerToSave.Name;

            await _producerRepository.UpdateAsync(producer);
        }

        public async Task Add(SaveProducerViewModel producerToCreate)
        {
            Producer producer = new();
            producer.Id = producerToCreate.Id;
            producer.Name = producerToCreate.Name;

            await _producerRepository.AddAsync(producer);
        }

        public async Task Delete(int id)
        {
            var producer = await _producerRepository.GetByIdAsync(id);
            await _producerRepository.DeleteAsync(producer);
        }
    }
}

