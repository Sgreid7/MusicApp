using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicApp.Models
{
  public partial class DatabaseContext : DbContext
  {

    // Add Database tables here
    public DbSet<Album> Albums { get; set; }

    public DbSet<Band> Bands { get; set; }

    public DbSet<Song> Songs { get; set; }

    public DbSet<Musician> Musicians { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql("server=localhost;database=MusicApp");
      }
    }
  }
}
