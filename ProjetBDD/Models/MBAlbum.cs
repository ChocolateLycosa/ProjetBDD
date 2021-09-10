using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.Models
{
    public class MBAlbum
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string ReleaseDate { get; set; }
        public string AlbumGenre { get; set; }
        public static MBAlbum FromJson(JObject album)
        {
            MBAlbum res = new MBAlbum();
            res.AlbumId = album.Value<int>("album_id");
            res.AlbumName = album.Value<string>("album_name");
            res.ReleaseDate = album.Value<string>("album_release_date");
            res.ArtistName = album.Value<string>("artist_name");
            JObject primary_genres = album.Value<JObject>("primary_genres");
            JArray genre_list = primary_genres.Value<JArray>("music_genre_list");
            string genre_str = "";
            foreach (JObject item in genre_list)
            {
                JObject info = item.Value<JObject>("music_genre");
                genre_str += info.Value<string>("music_genre_name");
                if (genre_list.IndexOf(item) < genre_list.Count - 1)
                {
                    genre_str += " / ";
                }
            }
            res.AlbumGenre = genre_str;
            return res;
        }
    }
}
