using Microsoft.EntityFrameworkCore;
using ProjetBDD.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.EntityModels
{
    public class SongsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlite(DataResource.DatabasePath);
        }

        public void VerifTables()
        {
            Database.ExecuteSqlRaw(
                "CREATE TABLE IF NOT EXISTS Artists(" +
                    "ArtistName TEXT PRIMARY KEY, " +
                    "ArtistCountry TEXT);");
            Database.ExecuteSqlRaw(
                "CREATE TABLE IF NOT EXISTS Albums (" +
                    "AlbumId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "AlbumName TEXT, " +
                    "AlbumGenre TEXT, " +
                    "ReleaseDate TEXT, " +
                    "ArtistName TEXT, " +
                    "FOREIGN KEY(ArtistName) REFERENCES Artists(ArtistName));");
            Database.ExecuteSqlRaw(
                "CREATE TABLE IF NOT EXISTS Titles (" +
                    "TitleId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "TitleName TEXT, " +
                    "AlbumId INTEGER, " +
                    "Lyrics TEXT, " +
                    "SongsterrId INTEGER, " +
                    "FOREIGN KEY(AlbumId) REFERENCES Albums(AlbumId));");
        }


        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
    }
}
