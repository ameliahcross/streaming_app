using System;
using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class GenreService
	{
		private readonly GenreRepository _genreRepository;

		public GenreService(ApplicationContext dbContext)
		{
			_genreRepository = new(dbContext);
		}


        public async Task<List<GenreViewModel>> GetAllViewModel()
        {
			var genrelist = await _genreRepository.GetAllAsync();

			return genrelist.Select(genre => new GenreViewModel
			{
				Name = genre.Name
			}).ToList();
        }
    }
}

