using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class SaveShowViewModel
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
    }
}