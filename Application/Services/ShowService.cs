using System;
using Application.Repository;
using Application.ViewModels;
using Database;

namespace Application.Services
{
	public class ShowService
	{
		private readonly ShowRepository _showRepository;

		public ShowService(ApplicationContext dbContext)
		{
			_showRepository = new(dbContext);
		}

        public async Task<List<ShowViewModel>> GetAllViewModel()
        {
            var showList = await _showRepository.GetAllAsync();

            return showList.Select(show => new ShowViewModel
            {
                Id = show.Id,
                Name = show.Name,
                ImageUrl = show.ImageUrl,
                VideoUrl = show.VideoUrl,
                ProducerName = show.Producer.Name,
                PrimaryGenreName = show.Genres.FirstOrDefault().Name,
                SecondaryGenreName = show.Genres.Skip(1).FirstOrDefault()?.Name,

            }).ToList();
        }

        public async Task<ShowViewModel> GetByIdAsync(int id)
        {
            //var show = await _showRepository.GetByIdAsync(id);

            //var showViewModel = new ShowViewModel
            //{
            //    Name = show.Name,
            //    VideoUrl = show.VideoUrl
            //};

            //return showViewModel;

            var show = await _showRepository.GetByIdAsync(id);
            ShowViewModel showViewModel = new();
            showViewModel.Id = show.Id;
            showViewModel.Name = show.Name;
            showViewModel.VideoUrl = show.VideoUrl;
            return showViewModel;
        }
    }
}

