using ProjetBDD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetBDD.EntityModels
{
    public class Artist
    {
        [Key]
        public string ArtistName { get; set; }

        public string ArtistCountry { get; set; }

        public static Artist FromAPIObject(MBArtist art)
        {
            Artist res = new Artist();
            res.ArtistName = art.ArtistName;
            res.ArtistCountry = art.ArtistCountry;
            return res;
        }
     
    }
}
