using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class ShowViewModel
	{
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

        public IEnumerable<GenreViewModel> Genres { get; set; }
        public IEnumerable<ProducerViewModel> Producers { get; set; }

        public ShowViewModel()
        {
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
    }
}

