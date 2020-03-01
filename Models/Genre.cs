using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class Genre
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public List<SongGenre> SongGenres { get; set; }
  }
}