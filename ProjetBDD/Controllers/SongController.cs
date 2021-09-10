using Microsoft.AspNetCore.Mvc;
using ProjetBDD.EntityModels;
using ProjetBDD.Models;
using ProjetBDD.Providers;
using ProjetBDD.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetBDD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private MusixBrainzProvider _MusixBrainz = null;
        private SongsterrProvider _Songsterr = null;
        private SongsRepository _Repository = null;
        private SongsContext _db;

        public SongsRepository Repository
        {
            get
            {
                if (_Repository == null)
                {
                    _Repository = new SongsRepository(db);
                }
                return _Repository;
            }
        }

        public SongsContext db {
            get
            { 
                if (_db == null)
                {
                    _db = new SongsContext();
                }
                return _db;
            }
        }

        private MusixBrainzProvider MusixBrainz
        {
            get
            {
                if (_MusixBrainz == null)
                {
                    _MusixBrainz = new MusixBrainzProvider();
                }
                return _MusixBrainz;
            }
        }

        private SongsterrProvider Songsterr
        {
            get
            {
                if (_Songsterr == null)
                {
                    _Songsterr = new SongsterrProvider();
                }
                return _Songsterr;
            }
        }



        // POST api/<SongController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SongInfo model)
        {
            MBArtist art = await MusixBrainz.GetArtist(model.Artist);
            MBAlbum alb = await MusixBrainz.GetAlbum(art.ArtistId, model.Album);
            MBTitle song = await MusixBrainz.GetTitle(alb.AlbumId, model.Titre);
            STitle title = await Songsterr.GetSongTab(art.ArtistName, song.Name);
            Artist artist = Artist.FromAPIObject(art);
            Repository.Post(db, artist);
            Album album = Album.FromAPIObject(alb);
            Repository.Post(db, album);
            Title titre = Title.FromAPIObject(song, title);
            titre.AlbumId = album.AlbumId;
            Repository.Post(db, titre);
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<object> list = Repository.Get(db);
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            Title res = Repository.Get(db, id);
            return Ok(res);
        }
    }
}
