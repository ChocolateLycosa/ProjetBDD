using ProjetBDD.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetBDD.EntityModels
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        public int AlbumId { get; set; }
        
        public string TitleName { get; set; }

        public int SongsterrId { get; set; }

        public string Lyrics { get; set; }

        public string SongsterrTabUrl
        {
            get
            {
                return $"http://www.songsterr.com/a/wa/song?id={SongsterrId}";
            }
        }


        public static Title FromAPIObject(MBTitle title, STitle tab)
        {
            Title res = new Title();
            res.AlbumId = title.AlbumId;
            res.Lyrics = title.Lyrics;
            res.SongsterrId = tab.Id;
            res.TitleName = title.Name;
            return res;
        }



    }
}
