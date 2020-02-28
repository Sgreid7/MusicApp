using System;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class Band
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string CountryOfOrigin { get; set; }

    public string NumberOfMembers { get; set; }

    public string Website { get; set; }

    public string Style { get; set; }

    public bool isSigned { get; set; }

    public string PersonOfContact { get; set; }

    public string ContactPhoneNumber { get; set; }

    // NAVIGATION PROPERTIES
    public List<Album> Albums { get; set; }

  }
}