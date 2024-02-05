using System;
using System.ComponentModel.DataAnnotations;
using Database.Models;

namespace Application.ViewModels
{
	public class SaveShowViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar un enlace para la imagen")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Debe colocar un enlace para el video")]
        public string VideoUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una productora")]
        public int ProducerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el género primario")]
        public int PrimaryGenreId { get; set; }

        public int? SecondaryGenreId { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }
        public IEnumerable<ProducerViewModel> Producers { get; set; }

        public SaveShowViewModel()
        {
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
    }
}