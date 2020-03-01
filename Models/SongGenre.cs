using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class SongGenre
  {
    public int Id { get; set; }

    // NAVIGATION PROPERTIES
    public int SongId { get; set; }

    public Song Song { get; set; }

    public int GenreId { get; set; }

    public Genre Genre { get; set; }
  }
}