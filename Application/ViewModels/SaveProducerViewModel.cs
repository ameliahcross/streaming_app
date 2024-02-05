using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class SaveProducerViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre para la productora")]
        public string Name { get; set; }
    }
}

