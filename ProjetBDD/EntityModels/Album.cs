using ProjetBDD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.EntityModels
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }

        public string AlbumGenre { get; set; }

        public string ArtistName { get; set; }

        public string ReleaseDate { get; set; }

        public static Album FromAPIObject(MBAlbum art)
        {
            Album res = new Album();
            res.ArtistName = art.ArtistName;
            res.AlbumName = art.AlbumName;
            res.AlbumGenre = art.AlbumGenre;
            res.ReleaseDate = art.ReleaseDate;
            return res;
        }
    }
}
