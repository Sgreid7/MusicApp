using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class RecordLabelManager
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();
    // ********** BANDS ************
    // CREATE BAND METHODS HERE TO CALL IN PROGRAM
    // SHOW BANDS 
    public void ShowBands()
    {
      var bands = db.Bands.OrderBy(b => b.Name);
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine($"Styles: {band.Styles}");
        Console.WriteLine($"Signed: {band.isSigned}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW ALL SIGNED BANDS
    public void ShowSignedBands()
    {
      var bands = db.Bands.Where(b => b.isSigned).OrderBy(b => b.Name);
      Console.WriteLine("Here are our current signed bands:");
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine("-----------------------------------");
      }
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // SHOW ALL BANDS NOT SIGNED
    public void ShowUnsignedBands()
    {
      var bands = db.Bands.Where(b => !b.isSigned).OrderBy(b => b.Name);
      Console.WriteLine("Here are all the unsigned bands:");
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine($"Styles: {band.Styles}");
        Console.WriteLine("-----------------------------------");
      }
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // ADD BAND TO DATABASE
    public void AddBandToDb(string name, string origin, string members, string website, List<Style> styles, string manager, string phoneNumber)
    {
      var band = new Band()
      {
        Name = name,
        CountryOfOrigin = origin,
        NumberOfMembers = members,
        Website = website,
        Styles = styles,
        isSigned = true,
        PersonOfContact = manager,
        ContactPhoneNumber = phoneNumber
      };
      // Add band to database
      db.Bands.Add(band);
      // Save changes
      db.SaveChanges();
    }

    // ADD SONG TO DATABASE
    public int AddSongToDb(int albumId, string title, string lyrics, TimeSpan length)
    {
      var album = db.Albums.First(album => album.Id == albumId);
      var songToAdd = new Song()
      {
        Title = title,
        Lyrics = lyrics,
        Length = length
      };
      album.Songs.Add(songToAdd);
      db.SaveChanges();
      return songToAdd.Id;
    }

    // ************ ALBUMS **************
    // CREATE ALBUM METHODS HERE TO CALL IN PROGRAM
    // PRODUCE AN ALBUM (CREATE)
    public int ProduceAlbum(string albumTitle, bool answer, DateTime releaseDate, int bandSelected)
    {
      var band = db.Bands.First(b => b.Id == bandSelected);
      // Create album with info above
      var albumToAdd = new Album()
      {
        Title = albumTitle,
        IsExplicit = answer,
        ReleaseDate = releaseDate
      };
      // Add album to database
      band.Albums.Add(albumToAdd);
      db.SaveChanges();
      return albumToAdd.Id;
    }
    // SHOW ALBUMS FOR A BAND
    public void ShowBandAlbums(int bandId)
    {
      var albums = db.Albums.Where(a => a.BandId == bandId).OrderBy(a => a.Title);
      foreach (var album in albums)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {album.Id}");
        Console.WriteLine($"Title: {album.Title}");
        Console.WriteLine($"Is explicit: {album.IsExplicit}");
        Console.WriteLine($"Release date: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW SONGS FROM AN ALBUM
    public void ShowAlbumSongs(int albumId)
    {
      var songs = db.Songs.Where(s => s.AlbumId == albumId).OrderBy(s => s.Title);
      foreach (var song in songs)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {song.Id}");
        Console.WriteLine($"Title: {song.Title}");
        Console.WriteLine($"Lyrics: {song.Lyrics}");
        Console.WriteLine($"Length: {song.Length}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW ALL ALBUMS
    public void ShowAllAlbums()
    {
      var albums = db.Albums.OrderBy(a => a.ReleaseDate);
      foreach (var a in albums)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {a.Id}");
        Console.WriteLine($"Title: {a.Title}");
        Console.WriteLine($"Is explicit: {a.IsExplicit}");
        Console.WriteLine($"Release date: {a.ReleaseDate.ToString("MM/dd/yyyy")}");
        Console.WriteLine("-----------------------------------");
      }
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

  }

}



