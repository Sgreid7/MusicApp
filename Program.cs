using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicApp
{
  class Program
  {
    // CREATE TIMESPAN
    static TimeSpan CreateTimeSpan(int hours, int minutes, int seconds)
    {
      TimeSpan songLength = new TimeSpan(hours, minutes, seconds);
      return songLength;
    }

    // Create reference to the record label manager class
    static public RecordLabelManager RLM { get; set; } = new RecordLabelManager();

    static void Main(string[] args)
    {
      // * * * * * CREATE CONSOLE MENU * * * * *
      var subMenu = new ConsoleMenu(args, level: 1)
          .Add("View a band's albums", () => ViewBandAlbums())
          .Add("View all albums", () => RLM.ShowAllAlbums())
          .Add("View an album's songs", () => ViewAlbumSongs())
          .Add("View all signed bands", () => RLM.ShowSignedBands())
          .Add("View all unsigned bands", () => RLM.ShowUnsignedBands())
          .Add("Sub_Close", ConsoleMenu.Close)
          .Add("Sub_Exit", () => Environment.Exit(0))
          .Configure(config =>
          {
            config.Selector = "==> ";
            config.EnableFilter = false;
            config.Title = "Submenu";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
          });

      var menu = new ConsoleMenu(args, level: 0)
      .Add("Sign a band", () => SignBand())
      .Add("Produce an album", () => CreateAlbum())
      .Add("Let go of a band", () => ReleaseBand())
      .Add("Re-sign a band", () => ReSignBand())
      .Add("VIEW other options", subMenu.Show)
      .Add("Close", ConsoleMenu.Close)
      .Add("Exit", () => Environment.Exit(0))
      .Configure(config =>
      {
        config.Selector = "==> ";
        config.EnableFilter = false;
        config.Title = "Music App Menu";
        config.EnableWriteTitle = true;
        config.EnableBreadcrumb = true;
      });

      menu.Show();
    }

    // * * * * * SIGN A BAND * * * * *
    static void SignBand()
    {
      // Define a genres list to add to the band
      var styles = new List<Style>();
      // Get band name
      Console.WriteLine("What band would you like to sign to our label?");
      var name = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      // Get bands country
      Console.WriteLine("What country is this band from?");
      var origin = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      // Get number of band members
      Console.WriteLine("How many members are in the band?");
      var members = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      // Get band website
      Console.WriteLine("What's the bands website?");
      var website = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      var addingStyles = true;
      // Get the bands style
      Console.WriteLine("What is the band's style?");
      var bandStyle = Console.ReadLine().ToLower();
      while (addingStyles)
      {
        var styleToAdd = new Style()
        {
          Name = bandStyle
        };
        // Add the style to the list of styles
        styles.Add(styleToAdd);
        Console.WriteLine("Would you like to add another style?");
        var userInput = Console.ReadLine().ToLower();
        while (userInput != "yes" && userInput != "no")
        {
          Console.WriteLine("Invalid input, please try again");
          userInput = Console.ReadLine().ToLower();
        }
        if (userInput == "yes")
        {
          Console.WriteLine("Please enter another style for the band.");
          bandStyle = Console.ReadLine().ToLower();
          styleToAdd = new Style()
          {
            Name = bandStyle
          };
        }
        else
        {
          addingStyles = false;
        }
        styles.Add(styleToAdd);
      }
      // Add space
      System.Console.WriteLine();
      // Get the band manager
      Console.WriteLine("Who is the band's manager?");
      var manager = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      // Get bands contact number
      Console.WriteLine("What's the bands contact number?");
      var phoneNumber = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      // Create that band with variables above
      RLM.AddBandToDb(name, origin, members, website, styles, manager, phoneNumber);
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // * * * * * PRODUCE ALBUM * * * * *    
    static void CreateAlbum()
    {
      var db = new DatabaseContext();
      // Ask which band album is for
      Console.WriteLine("Which band is this album for? Please choose the primary key.");
      // Show bands to user
      RLM.ShowBands();
      int bandSelected;
      var isInt = int.TryParse(Console.ReadLine(), out bandSelected);
      // Validate that band exists in database
      while (!db.Bands.Any(b => b.Id == bandSelected) && !isInt)
      {
        Console.WriteLine("Invalid input, please try again.");
        int.TryParse(Console.ReadLine(), out bandSelected);
      }
      Console.WriteLine("Enter the name of the new album:");
      var albumTitle = Console.ReadLine();
      // Add space
      System.Console.WriteLine();
      Console.WriteLine("Is this album explicit? (TRUE) or (FALSE)");
      bool answer;
      var isBool = Boolean.TryParse(Console.ReadLine(), out answer);
      while (!isBool)
      {
        Console.WriteLine("Not a valid input, please try again.");
        isBool = Boolean.TryParse(Console.ReadLine(), out answer);
      }
      // Add space
      System.Console.WriteLine();
      Console.WriteLine("When was this album released?");
      DateTime releaseDate;
      var isDate = DateTime.TryParse(Console.ReadLine(), out releaseDate);
      while (!isDate)
      {
        Console.WriteLine("Not a valid input, please try again.");
        isDate = DateTime.TryParse(Console.ReadLine(), out releaseDate);
      }
      // Create album with info above
      var albumId = RLM.ProduceAlbum(albumTitle, answer, releaseDate, bandSelected);
      // Add space
      System.Console.WriteLine();
      var addingSongs = true;
      // Create a while loop so they can continue adding songs to album until user chooses to stop
      while (addingSongs)
      {
        Console.WriteLine("Would you like to add a song to an album? (YES 'y') or (NO 'n')");
        var userInput = Console.ReadLine().ToLower();
        // validate answer
        while (userInput != "y" && userInput != "n")
        {
          Console.WriteLine("Not a valid input, please try again.");
          userInput = Console.ReadLine().ToLower();
        }
        if (userInput == "y")
        {
          // once album is found, gather info for the song
          Console.WriteLine("What is the song name?");
          var songName = Console.ReadLine();
          // Add space
          System.Console.WriteLine();
          Console.WriteLine("What are some of the lyrics for this song?");
          var lyrics = Console.ReadLine();
          // Add space
          System.Console.WriteLine();
          Console.WriteLine("How long is this song?");
          Console.WriteLine("Hours?");
          int hours;
          var isHour = int.TryParse(Console.ReadLine(), out hours);
          while (!isHour)
          {
            Console.WriteLine("Not a valid input, please try again.");
            isHour = int.TryParse(Console.ReadLine(), out hours);
          }
          Console.WriteLine("Minutes?");
          int minutes;
          var isMinute = int.TryParse(Console.ReadLine(), out minutes);
          while (!isMinute)
          {
            Console.WriteLine("Not a valid input, please try again.");
            isMinute = int.TryParse(Console.ReadLine(), out minutes);
          }
          Console.WriteLine("Seconds?");
          int seconds;
          var isSecond = int.TryParse(Console.ReadLine(), out seconds);
          while (!isSecond)
          {
            Console.WriteLine("Not a valid input, please try again.");
            isSecond = int.TryParse(Console.ReadLine(), out seconds);
          }
          var songLength = CreateTimeSpan(hours, minutes, seconds);
          var songId = RLM.AddSongToDb(albumId, songName, lyrics, songLength);
          // Add space
          System.Console.WriteLine();
          var addingGenres = true;
          var songGenres = new List<SongGenre>();
          while (addingGenres)
          {
            Console.WriteLine("Would you like to add a genre to this song? (YES) or (NO)");
            var input = Console.ReadLine().ToLower();
            // Validate user input
            while (input != "yes" && input != "no")
            {
              Console.WriteLine("Invalid input, please try again.");
              input = Console.ReadLine().ToLower();
            }
            if (input == "yes")
            {
              Console.WriteLine("What genre fits this song?");
              var genreName = Console.ReadLine();
              // Check if genre exists in database or not
              if (!db.Genres.Any())
              {
                var genreToAdd = new Genre()
                {
                  Name = genreName
                };
                db.Genres.Add(genreToAdd);
                db.SaveChanges();
              }
              var isGenre = db.Genres.Any(genre => genre.Name == genreName);
              // Initialize variable for genreId
              int genreId;
              // check if genre exists
              if (isGenre)
              {
                // Set the genreId
                genreId = db.Genres.First(genre => genre.Name == genreName).Id;
              }
              // else create it
              else
              {
                var genreToAdd = new Genre()
                {
                  Name = genreName
                };
                db.Genres.Add(genreToAdd);
                db.SaveChanges();
                genreId = db.Genres.First(genre => genre.Name == genreName).Id;
              }
              // Create a songGenre object with both a genre id and song id
              var songGenreToAdd = new SongGenre()
              {
                GenreId = genreId,
                SongId = songId
              };
              // Add to song genre list declared above
              songGenres.Add(songGenreToAdd);
            }
            else
            {
              addingGenres = false;
            }
          }
          db.Songs.First(song => song.Id == songId).SongGenres = songGenres;
          db.SaveChanges();
        }
        else
        {
          addingSongs = false;
        }
      }
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // * * * * * Let go of a band * * * * * 
    static void ReleaseBand()
    {
      var db = new DatabaseContext();
      Console.WriteLine("Which band should we release from our label? Please select the ID.");
      // Show bands to user
      RLM.ShowBands();
      int bandRemove;
      var exist = int.TryParse(Console.ReadLine(), out bandRemove);
      // make sure band exists
      while (!db.Bands.Any(b => b.Id == bandRemove) && !exist)
      {
        Console.WriteLine("Band does not exist, please try again.");
        int.TryParse(Console.ReadLine(), out bandRemove);
      }
      var bandLetGo = db.Bands.First(band => band.Id == bandRemove);
      bandLetGo.isSigned = false;
      db.SaveChanges();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // * * * * * Resign a band * * * * *
    static void ReSignBand()
    {
      var db = new DatabaseContext();
      Console.WriteLine("Which band should we resign? Please select the ID.");
      // Show user the bands
      RLM.ShowBands();
      int resignBand;
      var exist = int.TryParse(Console.ReadLine(), out resignBand);
      // make sure band exists
      while (!db.Bands.Any(b => b.Id == resignBand) && !exist)
      {
        Console.WriteLine("Band does not exist, please try again.");
        int.TryParse(Console.ReadLine(), out resignBand);
      }
      var bandToResign = db.Bands.First(band => band.Id == resignBand);
      bandToResign.isSigned = true;
      db.SaveChanges();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // * * * * * VIEW albums for a selected band * * * * *
    static void ViewBandAlbums()
    {
      var db = new DatabaseContext();
      Console.WriteLine("Which band's albums would you like to view? Please select the band ID.");
      // Show user the bands
      RLM.ShowBands();
      int viewBand;
      var exist = int.TryParse(Console.ReadLine(), out viewBand);
      while (!db.Bands.Any(b => b.Id == viewBand) && !exist)
      {
        Console.WriteLine("Band does not exist, please try again.");
        int.TryParse(Console.ReadLine(), out viewBand);
      }
      RLM.ShowBandAlbums(viewBand);
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    // * * * * * VIEW an albums songs * * * * *
    static void ViewAlbumSongs()
    {
      var db = new DatabaseContext();
      Console.WriteLine("Which album's songs would you like to view? Please select the album ID.");
      RLM.ShowAllAlbums();
      int albumChosen;
      var exist = int.TryParse(Console.ReadLine(), out albumChosen);
      // validate that album exists
      while (!db.Albums.Any(a => a.Id == albumChosen))
      {
        Console.WriteLine("Album does not exist, please try again.");
        int.TryParse(Console.ReadLine(), out albumChosen);
      }
      RLM.ShowAlbumSongs(albumChosen);
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
