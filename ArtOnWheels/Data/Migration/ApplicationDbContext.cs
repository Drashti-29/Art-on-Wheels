using ArtOnWheels.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ArtOnWheels.Data.Migration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Exhibition> Exhibitions { get; set; }
    }
}
