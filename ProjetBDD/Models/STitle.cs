using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetBDD.Models
{
    public class STitle
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static STitle FromJSON(JObject item)
        {
            STitle title = new STitle();
            title.Id = item.Value<int>("id");
            title.Title = item.Value<string>("title");
            return title;
            
        }
    }
}
