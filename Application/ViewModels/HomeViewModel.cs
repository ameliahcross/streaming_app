using System;
namespace Application.ViewModels
{
	public class HomeViewModel
	{
        // Listado de series para mostrar en el home
        public IEnumerable<ShowViewModel> Shows { get; set; }

        // Listado de productoras para mostrar en el home
        public IEnumerable<ProducerViewModel> Producers { get; set; }

        // Opciones de genero para el filtro
        public IEnumerable<GenreViewModel> Genres { get; set; }
        public int? SelectedGenreName{ get; set; }

        // Propiedad para la búsqueda por nombre de serie
        public string SearchByName { get; set; }

        public HomeViewModel()
		{
            Shows = new List<ShowViewModel>();
            Genres = new List<GenreViewModel>();
            Producers = new List<ProducerViewModel>();
        }
	}
}

