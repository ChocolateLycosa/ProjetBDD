using Newtonsoft.Json.Linq;
using ProjetBDD.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ProjetBDD.Providers
{
    public class MusixBrainzProvider : HttpClient
    {
        public async Task<MBArtist> GetArtist(string artistName)
        {
            HttpResponseMessage response = await GetAsync("https://api.musixmatch.com/ws/1.1/artist.search?apikey=7297745482604b5f4a1a990b07c42860&q_artist=Periphery&format=json");
            string payload = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(payload);
            JArray List = json
                .Value<JObject>("message")
                .Value<JObject>("body")
                .Value<JArray>("artist_list");
            List<MBArtist> retenus = new List<MBArtist>();
            foreach (JObject item in List)
            {
                JObject artist = item.Value<JObject>("artist");
                if (artist.Value<string>("artist_name") == artistName)
                {
                    retenus.Add(MBArtist.FromJson(artist));
                }
            }
            if (retenus.Count == 0)
            {
                throw new Exception("Aucun artiste de ce type");
            }
            return retenus.First();
        }


        public async Task<MBAlbum> GetAlbum(int artistId, string album)
        {
            HttpResponseMessage response = await GetAsync($"https://api.musixmatch.com/ws/1.1/artist.albums.get?apikey=7297745482604b5f4a1a990b07c42860&artist_id={artistId}");
            string payload = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(payload);
            JArray List = json
                .Value<JObject>("message")
                .Value<JObject>("body")
                .Value<JArray>("album_list");
            List<MBAlbum> retenus = new List<MBAlbum>();
            foreach (JObject item in List)
            {
                JObject artist = item.Value<JObject>("album");
                if (artist.Value<string>("album_name") == album)
                {
                    retenus.Add(MBAlbum.FromJson(artist));
                }
            }
            if (retenus.Count == 0)
            {
                throw new Exception("Aucun album de ce type");
            }
            return retenus.First();
        }



        public async Task<MBTitle> GetTitle(int albumId, string titleName)
        {
            HttpResponseMessage response = await GetAsync($"https://api.musixmatch.com/ws/1.1/album.tracks.get?album_id={albumId}&apikey=7297745482604b5f4a1a990b07c42860");
            string payload = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(payload);
            List<MBTitle> retenus = new List<MBTitle>();
            JArray List = json
                .Value<JObject>("message")
                .Value<JObject>("body")
                .Value<JArray>("track_list");
            foreach (JObject item in List)
            {
                JObject title = item.Value<JObject>("track");
                if (title.Value<string>("track_name") == titleName)
                {
                    int track_id = title.Value<int>("track_id");
                    HttpResponseMessage lyricsResponse = await GetAsync($"https://api.musixmatch.com/ws/1.1/track.lyrics.get?apikey=7297745482604b5f4a1a990b07c42860&track_id={track_id}");
                    string lyricsPayload = await lyricsResponse.Content.ReadAsStringAsync();
                    JObject lyricsJson = JObject.Parse(lyricsPayload)
                        .Value<JObject>("message")
                        .Value<JObject>("body")
                        .Value<JObject>("lyrics");

                    retenus.Add(MBTitle.FromJSON(title, lyricsJson.Value<string>("lyrics_body")));
                }
            }
            if (retenus.Count == 0)
            {
                throw new Exception("Aucun titre de ce nom dans l'album");
            }
            return retenus.First();
        }
    }
}
