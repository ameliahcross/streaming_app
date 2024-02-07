
using System;
using Database.Models;

namespace Application.ViewModels
{
	public class FilterViewModel
	{
        public IEnumerable<ShowViewModel> Shows { get; set; }
        public IEnumerable<ProducerViewModel> Producers { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }

        public int? SelectedGenreId { get; set; }
        public int? SelectedProducerId { get; set; }
        public string SearchByName { get; set; }

        public FilterViewModel()
		{
            Shows = new List<ShowViewModel>();
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
	}
}

