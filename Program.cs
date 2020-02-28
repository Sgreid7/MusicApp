using System;
using MusicApp.Models;
using System.Linq;
using ConsoleTools;


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

    // SHOW BANDS 
    static void ShowBands()
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
    static void ShowSignedBands()
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
    static void ShowUnsignedBands()
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

    // SHOW ALBUMS
    static void ShowBandAlbums(int bandId)
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
    static void ShowAlbumSongs(int albumId)
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
    static void ShowAllAlbums()
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
    static void Main(string[] args)
    {
      // CONNECT TO DATABASE
      var db = new DatabaseContext();
      // Create an isRunning variable for program while loop
      var isRunning = true;
      // Greet user
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Welcome to 3DOT Recordings!");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      System.Console.WriteLine();

      while (isRunning)
      {
        // Give the user options to choose from
        Console.WriteLine("What would you like to do today for our label?");
        Console.WriteLine("(SIGN 's') a new band, (PRODUCE 'p') an album, (LET GO 'l') of a band, (RESIGN 'r') a band");
        Console.WriteLine("(VIEW 'v') albums for a band, (VIEW 'a') all albums, (VIEW 'x') an album's songs");
        Console.WriteLine("(VIEW 'b') signed bands, (VIEW 'n') bands not signed, or (QUIT 'q')");


        // var menu = new ConsoleMenu(args, level: 0)
        // .Add("Sign a band", () => SomeAction("One"))
        // .Add("Produce an album", () => SomeAction("Two"))
        // .Add("Let go of a band", () => SomeAction("Three"))
        // .Add("re-sign a band", subMenu.Show)
        // .Add("Close", ConsoleMenu.Close)
        // .Add("Action then Close", (thisMenu) => { SomeAction("Closing action..."); thisMenu.CloseMenu(); })
        // .Add("Exit", () => Environment.Exit(0))
        // .Configure(config =>
        // {
        //   config.Selector = "--> ";
        //   config.EnableFilter = true;
        //   config.Title = "Main menu";
        //   config.EnableWriteTitle = true;
        //   config.EnableBreadcrumb = true;
        // });

        // Get the user input
        var input = Console.ReadLine().ToLower();
        // VALIDATE THE USER INPUT TO MAKE SURE IT'S A VALID INPUT
        while (input != "s" && input != "p" && input != "l" && input != "r" && input != "v" && input != "a" && input != "x" && input != "b" && input != "n" && input != "q")
        {
          Console.WriteLine("That is not a valid input, please try again.");
          input = Console.ReadLine().ToLower();
        }
        // Add space
        Console.WriteLine();
        // create switch statement based on user input
        switch (input)
        {
          // * * * * * SIGN A BAND
          case "s":
            // Get band name
            Console.WriteLine("What band would you like to sign to our label?");
            var bandName = Console.ReadLine();
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
            var band = new Band()
            {
              Name = bandName,
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
            break;

          case "p":
            // * * * * * PRODUCE
            // Ask which band album is for
            Console.WriteLine("Which band is this album for? Please choose the ID.");
            // Show bands to user
            ShowBands();
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
            var album = new Album()
            {
              Title = newAlbum,
              IsExplicit = answer,
              ReleaseDate = releaseDate,
              BandId = bandSelected
            };
            // Add album to database
            db.Albums.Add(album);
            db.SaveChanges();


            Console.WriteLine("Would you like to add a song to an album? (YES 'y') or (NO 'n')");
            var userInput = Console.ReadLine().ToLower();
            // validate answer
            while (userInput != "y" && userInput != "n")
            {
              Console.WriteLine("Not a valid input, please try again.");
              userInput = Console.ReadLine().ToLower();
            }
            var addingSongs = true;
            // Create a while loop so they can continue adding songs to album until user chooses to stop
            while (addingSongs)
            {
              if (userInput == "y")
              {
                // Get info from user for a song to add to that album
                // Figure out which album to add song to
                Console.WriteLine("What album would you like to add this song to? Please choose the ID.");
                ShowBandAlbums(bandSelected);
                int albumSelected;
                var isValid = int.TryParse(Console.ReadLine(), out albumSelected);
                // Check if album exists in database 
                while (!db.Albums.Any(a => a.Id == albumSelected) && !isValid)
                {
                  Console.WriteLine("That album does not exist or invalid input, please try again.");
                  int.TryParse(Console.ReadLine(), out albumSelected);
                }
                // once album is found, gather info for the song
                Console.WriteLine("What is the song name for this album?");
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
                }
                Console.WriteLine("Minutes?");
                int minutes;
                var isMinute = int.TryParse(Console.ReadLine(), out minutes);
                while (!isMinute)
                {
                  Console.WriteLine("Not a valid input, please try again.");
                }
                Console.WriteLine("Seconds?");
                int seconds;
                var isSecond = int.TryParse(Console.ReadLine(), out seconds);
                while (!isSecond)
                {
                  Console.WriteLine("Not a valid input, please try again.");
                }
                var songLength = CreateTimeSpan(hours, minutes, seconds);
                // Add space
                System.Console.WriteLine();
                Console.WriteLine("What genre is this song?");
                var songGenre = Console.ReadLine();
                // Add space
                System.Console.WriteLine();
                var song = new Song()
                {
                  Title = songName,
                  Lyrics = lyrics,
                  Length = songLength,
                  Genre = songGenre,
                  AlbumId = albumSelected
                };
                // Add song to songs table
                db.Songs.Add(song);
                db.SaveChanges();
              }
              else
              {
                addingSongs = false;
              }
            }
            break;

          // Let go of a band
          case "l":
            Console.WriteLine("Which band should we release from 3DOT Recordings? Please select the ID.");
            // Show bands to user
            ShowBands();
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
            break;

          // * * * * * Resign a band
          case "r":
            Console.WriteLine("Which band should we resign? Please select the ID.");
            // Show user the bands
            ShowBands();
            int resignBand;
            exist = int.TryParse(Console.ReadLine(), out resignBand);
            // make sure band exists
            while (!db.Bands.Any(b => b.Id == resignBand) && !exist)
            {
              Console.WriteLine("Band does not exist, please try again.");
              int.TryParse(Console.ReadLine(), out resignBand);
            }

            var bandToResign = db.Bands.First(band => band.Id == resignBand);
            bandToResign.isSigned = true;
            db.SaveChanges();
            break;

          // * * * * * VIEW albums for a selected band 
          case "v":
            Console.WriteLine("Which band's albums would you like to view? Please select the band ID.");
            // Show user the bands
            ShowBands();
            int viewBand;
            exist = int.TryParse(Console.ReadLine(), out viewBand);
            while (!db.Bands.Any(b => b.Id == viewBand) && !exist)
            {
              Console.WriteLine("Band does not exist, please try again.");
              int.TryParse(Console.ReadLine(), out viewBand);
            }
            ShowBandAlbums(viewBand);
            break;

          // * * * * * VIEW ALL ALBUMS FROM ALL BANDS
          case "a":
            ShowAllAlbums();
            break;

          // * * * * * VIEW an albums songs
          case "x":
            Console.WriteLine("Which album's songs would you like to view? Please select the album ID.");
            ShowAllAlbums();
            int albumChosen;
            exist = int.TryParse(Console.ReadLine(), out albumChosen);
            // validate that album exists
            while (!db.Albums.Any(a => a.Id == albumChosen) && !exist)
            {
              Console.WriteLine("Album does not exist, please try again.");
              int.TryParse(Console.ReadLine(), out albumChosen);
            }
            ShowAlbumSongs(albumChosen);
            break;

          // * * * * * VIEW signed bands
          case "b":
            ShowSignedBands();
            break;


          // * * * * * VIEW unsigned bands
          case "n":
            ShowUnsignedBands();
            break;

          // * * * * * QUIT
          case "q":
            Console.WriteLine("Thanks for stopping by 3DOT Recordings today!");
            isRunning = false;
            break;

        }

      }

    }
  }
}
