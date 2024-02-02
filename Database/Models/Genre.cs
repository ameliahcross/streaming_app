using System;
namespace Database.Models
{
	public class Genre
	{
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation properties
        public ICollection<Show> Shows { get; set; }
    }
}

