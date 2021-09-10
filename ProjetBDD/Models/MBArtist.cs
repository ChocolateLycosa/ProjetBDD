using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.Models
{
    public class MBArtist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistCountry { get; set; }
        public static MBArtist FromJson(JObject artist)
        {
            MBArtist res = new MBArtist();
            res.ArtistId = artist.Value<int>("artist_id");
            res.ArtistName = artist.Value<string>("artist_name");
            res.ArtistCountry = artist.Value<string>("artist_country");
            return res;
        }
    }
}
