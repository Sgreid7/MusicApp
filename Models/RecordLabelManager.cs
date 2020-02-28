using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

namespace MusicApp.Models
{
  public class RecordLabelManager
  {
    // public DatabaseContext db { get; set; } = new DatabaseContext();
    // ********** BANDS ************
    // CREATE BAND METHODS HERE TO CALL IN PROGRAM
    // SHOW BANDS 
    public void ShowBands()
    {
      var db = new DatabaseContext();
      var bands = db.Bands.OrderBy(b => b.Name);
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine($"Style: {band.Style}");
        Console.WriteLine($"Signed: {band.isSigned}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW ALL SIGNED BANDS
    public void ShowSignedBands()
    {
      var db = new DatabaseContext();
      var bands = db.Bands.Where(b => b.isSigned).OrderBy(b => b.Name);
      Console.WriteLine("Here are our current signed bands:");
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine($"Style: {band.Style}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW ALL BANDS NOT SIGNED
    public void ShowUnsignedBands()
    {
      var db = new DatabaseContext();
      var bands = db.Bands.Where(b => !b.isSigned).OrderBy(b => b.Name);
      Console.WriteLine("Here are all the unsigned bands:");
      foreach (var band in bands)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {band.Id}");
        Console.WriteLine($"Name: {band.Name}");
        Console.WriteLine($"Country: {band.CountryOfOrigin}");
        Console.WriteLine($"Number of members: {band.NumberOfMembers}");
        Console.WriteLine($"Style: {band.Style}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // ADD BAND TO DATABASE
    public void AddBandToDb(string name, string origin, string members, string website, string genre, string manager, string phoneNumber)
    {
      var db = new DatabaseContext();
      var band = new Band()
      {
        Name = name,
        CountryOfOrigin = origin,
        NumberOfMembers = members,
        Website = website,
        Style = genre,
        isSigned = true,
        PersonOfContact = manager,
        ContactPhoneNumber = phoneNumber
      };
      // Add band to database
      db.Bands.Add(band);
      // Save changes
      db.SaveChanges();
    }

    // ************ ALBUMS **************
    // CREATE ALBUM METHODS HERE TO CALL IN PROGRAM
    // PRODUCE AN ALBUM (CREATE)
    public void ProduceAlbum(string newAlbum, bool answer, DateTime releaseDate, int bandSelected, List<Song> newSongs)
    {
      var db = new DatabaseContext();
      // Create album with info above
      var album = new Album()
      {
        Title = newAlbum,
        IsExplicit = answer,
        ReleaseDate = releaseDate,
        BandId = bandSelected,
        Songs = newSongs
      };
      // Add album to database
      db.Albums.Add(album);
      db.SaveChanges();
    }
    // SHOW ALBUMS FOR A BAND
    public void ShowBandAlbums(int bandId)
    {
      var db = new DatabaseContext();
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
      var db = new DatabaseContext();
      var songs = db.Songs.Where(s => s.AlbumId == albumId).OrderBy(s => s.Title);
      foreach (var song in songs)
      {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Primary Key: {song.Id}");
        Console.WriteLine($"Title: {song.Title}");
        Console.WriteLine($"Length: {song.Length}");
        Console.WriteLine($"Genre: {song.Genre}");
        Console.WriteLine("-----------------------------------");
      }
    }

    // SHOW ALL ALBUMS
    public void ShowAllAlbums()
    {
      var db = new DatabaseContext();
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
    }

    // ******** CREATE SONG **********
    public Song CreateSong(string songName, string lyrics, TimeSpan songLength, string songGenre)
    {
      var db = new DatabaseContext();
      return new Song()
      {
        Title = songName,
        Lyrics = lyrics,
        Length = songLength,
        Genre = songGenre
      };
      // Add song to songs table
      //   db.Songs.Add(song);
      //   db.SaveChanges();

    }


  }

}



