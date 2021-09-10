using Microsoft.EntityFrameworkCore;
using ProjetBDD.EntityModels;
using ProjetBDD.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetBDD.Repositories
{
    public class SongsRepository
    {
        public SongsRepository(SongsContext db)
        {
            db.VerifTables();
        }
        public void Post(SongsContext db, Artist art)
        {
            db.Entry(art).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Post(SongsContext db, Album artist)
        {
            db.Entry(artist).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Post(SongsContext db, Title title)
        {
            db.Entry(title).State = EntityState.Added;
            db.SaveChanges();
        }

        public Title Get(SongsContext db, int id)
        {
            return db.Titles.Find(id);
        }

        public IEnumerable<object> Get(SongsContext db)
        {
            return from ar in db.Artists
                    join alb in db.Albums on ar.ArtistName equals alb.ArtistName
                    join title in db.Titles on alb.AlbumId equals title.AlbumId
                   select new 
                   { 
                       Id = title.TitleId,
                       ar.ArtistName,
                       ar.ArtistCountry,
                       alb.AlbumName,
                       alb.AlbumGenre,
                       alb.ReleaseDate,
                       title.TitleName
                   };
        }
    }
}
