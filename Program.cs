using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;
using System.Collections.Generic;

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
    static public RecordLabelManager rlManager { get; set; } = new RecordLabelManager();

    // CONNECT TO DATABASE
    // public DatabaseContext db { get; set; } = new DatabaseContext();

    static void Main(string[] args)
    {
      // CONNECT TO DATABASE
      var db = new DatabaseContext();
      // Create an isRunning variable for program while loop
      // var isRunning = true;

      // Greet user
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Welcome to Infinity Records!");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      System.Console.WriteLine();

      // while (isRunning)
      // {
      // Give the user options to choose from
      // Console.WriteLine("What would you like to do today for our label?");
      // Console.WriteLine("(SIGN 's') a new band, (PRODUCE 'p') an album, (LET GO 'l') of a band, (RESIGN 'r') a band");
      // Console.WriteLine("(VIEW 'v') albums for a band, (VIEW 'a') all albums, (VIEW 'x') an album's songs");
      // Console.WriteLine("(VIEW 'b') signed bands, (VIEW 'n') bands not signed, or (QUIT 'q')");

      static void SomeAction(string text)
      {
        Console.WriteLine(text);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
      }



      var subMenu = new ConsoleMenu(args, level: 1)
          .Add("View a band's albums", () => ViewBandAlbums())
          .Add("View all albums", () => rlManager.ShowAllAlbums())
          .Add("View an album's songs", () => ViewAlbumSongs())
          .Add("View all signed bands", () => rlManager.ShowSignedBands())
          .Add("View all unsigned bands", () => rlManager.ShowUnsignedBands())
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
      // .Add("Action then Close", (thisMenu) => { SomeAction("Closing action..."); thisMenu.CloseMenu(); })
      .Add("Exit", () => Environment.Exit(0))
      .Configure(config =>
      {
        config.Selector = "==> ";
        config.EnableFilter = false;
        config.Title = "Main menu";
        config.EnableWriteTitle = true;
        config.EnableBreadcrumb = true;
      });

      menu.Show();

      Console.WriteLine();

      // * * * * * SIGN A BAND * * * * *
      static void SignBand()
      {
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
        // Get the bands genre
        Console.WriteLine("What's the band genre?");
        var genre = Console.ReadLine();
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
        rlManager.AddBandToDb(name, origin, members, website, genre, manager, phoneNumber);
        Console.ReadKey();
      }

      // * * * * * PRODUCE * * * * *    

      static void CreateAlbum()
      {
        var db = new DatabaseContext();
        // Ask which band album is for
        Console.WriteLine("Which band is this album for? Please choose the primary key.");
        // Show bands to user
        rlManager.ShowBands();
        int bandSelected;
        var isInt = int.TryParse(Console.ReadLine(), out bandSelected);
        // Validate that band exists in database
        while (!db.Bands.Any(b => b.Id == bandSelected) && !isInt)
        {
          Console.WriteLine("Invalid input, please try again.");
          int.TryParse(Console.ReadLine(), out bandSelected);
        }
        Console.WriteLine("Enter the name of the new album:");
        var newAlbum = Console.ReadLine();
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
        // Add space
        System.Console.WriteLine();
        // Create album with info above
        // Create list of songs to add to the album
        var newSongs = new List<Song>();
        var addingSongs = true;
        // Create a while loop so they can continue adding songs to album until user chooses to stop
        while (addingSongs)
        {
          Console.WriteLine("Would you like to add a song to this album? (YES 'y') or (NO 'n')");
          var userInput = Console.ReadLine().ToLower();
          // validate answer
          while (userInput != "y" && userInput != "n")
          {
            Console.WriteLine("Not a valid input, please try again.");
            userInput = Console.ReadLine().ToLower();
          }
          if (userInput == "y")
          {
            // Get info from user for a song to add to that album
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
            // Add space
            System.Console.WriteLine();
            Console.WriteLine("What genre is this song?");
            var songGenre = Console.ReadLine();
            // Add space
            System.Console.WriteLine();
            var createdSong = rlManager.CreateSong(songName, lyrics, songLength, songGenre);
            newSongs.Add(createdSong);
          }
          else
          {
            addingSongs = false;
            // Add space
            Console.WriteLine();
          }
        }
        rlManager.ProduceAlbum(newAlbum, answer, releaseDate, bandSelected, newSongs);
        Console.ReadKey();
      }

      // * * * * * Let go of a band * * * * * 
      static void ReleaseBand()
      {
        var db = new DatabaseContext();
        Console.WriteLine("Which band should we release from Infinity Records? Please select the ID.");
        // Show bands to user
        rlManager.ShowBands();
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
        Console.ReadKey();
      }

      // * * * * * Resign a band * * * * *
      static void ReSignBand()
      {
        var db = new DatabaseContext();
        Console.WriteLine("Which band should we resign? Please select the ID.");
        // Show user the bands
        rlManager.ShowBands();
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
        Console.ReadKey();
      }

      // * * * * * VIEW albums for a selected band * * * * *
      static void ViewBandAlbums()
      {
        var db = new DatabaseContext();
        Console.WriteLine("Which band's albums would you like to view? Please select the band ID.");
        // Show user the bands
        rlManager.ShowBands();
        int viewBand;
        var exist = int.TryParse(Console.ReadLine(), out viewBand);
        while (!db.Bands.Any(b => b.Id == viewBand) && !exist)
        {
          Console.WriteLine("Band does not exist, please try again.");
          int.TryParse(Console.ReadLine(), out viewBand);
        }
        rlManager.ShowBandAlbums(viewBand);
        Console.ReadKey();
      }

      // * * * * * VIEW an albums songs * * * * *
      static void ViewAlbumSongs()
      {
        var db = new DatabaseContext();
        Console.WriteLine("Which album's songs would you like to view? Please select the album ID.");
        rlManager.ShowAllAlbums();
        int albumChosen;
        var exist = int.TryParse(Console.ReadLine(), out albumChosen);
        // validate that album exists
        while (!db.Albums.Any(a => a.Id == albumChosen) && !exist)
        {
          Console.WriteLine("Album does not exist, please try again.");
          int.TryParse(Console.ReadLine(), out albumChosen);
        }
        rlManager.ShowAlbumSongs(albumChosen);
        Console.ReadKey();
      }
    }
  }
}
