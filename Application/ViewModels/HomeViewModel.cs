using System;
namespace Application.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<ShowViewModel> Shows { get; set; }
        public IEnumerable<ProducerViewModel> Producers { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }
        public int? SelectedGenreName{ get; set; }
        public string SearchByName { get; set; }

        public HomeViewModel()
		{
            Shows = new List<ShowViewModel>();
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
	}
}

