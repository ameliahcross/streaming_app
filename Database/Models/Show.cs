using System;
namespace Database.Models
{
	public class Show
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public int ProducerId { get; set; } // foreign key

        // navigation properties
        public Producer Producer { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}

