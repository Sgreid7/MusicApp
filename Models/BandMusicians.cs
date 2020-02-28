namespace MusicApp.Models
{
  public class BandMusicians
  {
    public int Id { get; set; }

    // NAVIGATION PROPERTIES
    public int BandId { get; set; }

    public Band Band { get; set; }

    public int MusicianId { get; set; }

    public Musician Musician { get; set; }
  }
}