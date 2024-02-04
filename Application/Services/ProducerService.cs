using System;
using Application.Repository;
using Application.ViewModels;
using Database;

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
            var genrelist = await _producerRepository.GetAllAsync();

            return genrelist.Select(genre => new ProducerViewModel
            {
                Name = genre.Name
            }).ToList();
        }

    }
}

