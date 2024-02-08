using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<ShowViewModel> Shows { get; set; }
        public IEnumerable<ProducerViewModel> Producers { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }

        public int? SelectedGenreId{ get; set; }
        public int? SelectedProducerId { get; set; }
        public string SearchByName { get; set; }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        [Required]
        public int ProducerId { get; set; }
        [Required]
        public int PrimaryGenreId { get; set; }
        public int? SecondaryGenreId { get; set; }

        public string ProducerName { get; set; }
        public string PrimaryGenreName { get; set; }
        public string SecondaryGenreName { get; set; }

        public HomeViewModel()
		{
            Shows = new List<ShowViewModel>();
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
	}
}

