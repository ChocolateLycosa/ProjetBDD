using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.Models
{
    public class MBTitle
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Name { get; set; }

        public string Lyrics { get; set; }

        public static MBTitle FromJSON(JObject item, string lyrics)
        {
            MBTitle res = new MBTitle();
            res.Id = item.Value<int>("track_id");
            res.Name = item.Value<string>("track_name");
            res.AlbumId = item.Value<int>("album_id");
            res.Lyrics = lyrics;
            return res;
        }
    }
}
