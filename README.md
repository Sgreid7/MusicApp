# Music Madness

A "record label" console app that stores our artist information in a database.

# Objectives

- Create a console app that uses an ORM to talk to a database
- Working with EF Core
- Re-enforce SQL fundamentals
- One to many relationships
- Integrate 3rd party packages

# Includes

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [EF CORE](https://docs.microsoft.com/en-us/ef/core/)
- [POSTGRESQL](https://www.postgresql.org/)
- [CONSOLE MENU](https://www.nuget.org/packages/ConsoleMenu-simple/)
- [MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)

# Featured Code

## One to many relationship POCO

```JSX
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
```
