using System;
namespace Application.ViewModels
{
	public class ShowViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public int ProducerId { get; set; } 
        public int PrimaryGenreId { get; set; } 
        public int? SecondaryGenreId { get; set; } 
        
        public string ProducerName { get; set; } 
        public string PrimaryGenreName { get; set; } 
        public string SecondaryGenreName { get; set; } 

      
	}
}

