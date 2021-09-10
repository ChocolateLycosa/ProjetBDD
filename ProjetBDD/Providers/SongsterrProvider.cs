using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using ProjetBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjetBDD.Providers
{
    public class SongsterrProvider : HttpClient
    {
        public async Task<STitle> GetSongTab(string artistName, string songName)
        {
            HttpResponseMessage response = await GetAsync($"http://www.songsterr.com/a/ra/songs/byartists.json?artists={artistName}");
            string payload = await response.Content.ReadAsStringAsync();
            JArray json = JArray.Parse(payload);
            List<STitle> retenus = new List<STitle>();
            foreach (JObject item in json)
            {
                if (item.Value<string>("title") == songName)
                {
                    retenus.Add(STitle.FromJSON(item));
                }
            }
            if (retenus.Count == 0)
            {
                throw new Exception("Aucun titre de ce type");
            }
            return retenus.First();
        }
    }
}
