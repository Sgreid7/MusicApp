using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class BandStyles
  {
    public int Id { get; set; }
    public int BandId { get; set; }

    public Band Band { get; set; }

    public int StyleId { get; set; }

    public Style Style { get; set; }
  }
}