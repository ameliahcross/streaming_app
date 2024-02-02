using System;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Show> Shows { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API

            #region tables
            modelBuilder.Entity<Show>()
                .ToTable("Shows");

            modelBuilder.Entity<Producer>()
                .ToTable("Producers");

            modelBuilder.Entity<Genre>()
                .ToTable("Genres");
            #endregion

            #region primary-keys
            modelBuilder.Entity<Show>()
                .HasKey(show => show.Id);

            modelBuilder.Entity<Producer>()
                .HasKey(producer => producer.Id);

            modelBuilder.Entity<Genre>()
                .HasKey(genre => genre.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Genre>()
                .HasMany<Show>(genre => genre.Shows)
                .WithMany(show => show.Genres);

            modelBuilder.Entity<Producer>()
               .HasMany<Show>(producer => producer.Shows)
               .WithOne(show => show.Producer)
               .HasForeignKey(show => show.ProducerId)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region property configurations

                #region shows
                modelBuilder.Entity<Show>()
                    .Property(show => show.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                modelBuilder.Entity<Show>()
                    .Property(show => show.ImageUrl)
                    .IsRequired();

                modelBuilder.Entity<Show>()
                    .Property(show => show.VideoUrl)
                    .IsRequired();
                #endregion

                #region genres
                modelBuilder.Entity<Genre>()
                    .Property(genre => genre.Name)
                    .IsRequired()
                    .HasMaxLength(20);
                #endregion

                #region producers
                modelBuilder.Entity<Producer>()
                    .Property(producer => producer.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                #endregion

            #endregion
        }
    }
}

