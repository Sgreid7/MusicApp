using System;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class Album
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public bool IsExplicit { get; set; }

    public DateTime ReleaseDate { get; set; }

    // NAVIGATION PROPERTIES
    public int BandId { get; set; }

    public Band Band { get; set; }
    public List<Song> Songs = new List<Song>();
  }
}