using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class SaveGenreViewModel
	{
        public int Id { get; set; }
        [Required (ErrorMessage = "Debe colocar un nombre para el género")]
        public string Name { get; set; }
	}
}

