using System;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class Song
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public string Lyrics { get; set; }

    public TimeSpan Length { get; set; }

    public string Genre { get; set; }

    // NAVIGATION PROPERTIES
    public int AlbumId { get; set; }

    public Album Album { get; set; }
  }
}