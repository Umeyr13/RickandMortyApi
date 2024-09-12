using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RickMortyApplication.DTOs;
using RickMortyApplication.IServices;
using RickMortyDomain.Entities;
using System.Globalization;

namespace RickandMorty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IAppUserService _appUserService;
        private readonly ICharacterService _characterService;
        private readonly IEpisodeService _episodeService;
        private readonly ILocationService _locationService;
        private readonly ICharacterEpisodeService _characterEpisodeService;

        public HomeController(ILikeService likeService, IAppUserService appUserService, ICharacterService characterService, IEpisodeService episodeService, ILocationService locationService, ICharacterEpisodeService characterEpisodeService)
        {
            _likeService = likeService;
            _appUserService = appUserService;
            _characterService = characterService;
            _episodeService = episodeService;
            _locationService = locationService;
            _characterEpisodeService = characterEpisodeService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            //var result= await _appUserService.CreateUserAsync(new RickMortyDomain.Entities.AppUser { UserName = "asd", Email = "asd@d.com" }, "asd");

            return Ok();
        }

        //verileri alıp veri tabanına eklemek için tek seferlik kullanım amacıyla yapıldı.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ListCharacterDto> characters)
        {
            foreach (var c in characters)
            {
                var _character = await _characterService.GetCharacterById(c.id);
                foreach(var episode in c.episode)
                {
                    int index = episode.LastIndexOf("/");
                    int id = int.Parse(episode.Substring(index + 1));
                    var _episode = await _episodeService.GetEpisodeId(id);
                    await _characterEpisodeService.AddCharacterEpisodeAsync(new CharacterEpisode() { CharacterId = _character.Id, EpisodeId = id });              
                }          
          
               // var result = await _characterService.UpdateCharacter(_character);
            }

            //foreach (var e in episodes) {

            //    Episode _episode = new Episode()
            //    {
            //        Name = e.Name,
            //        Url = e.Url,
            //        EpisodeCode = e.Episode,
            //        AirDate = Convert.ToDateTime(e.Air_Date),
            //        CreatedDate = e.Created
            //    };

            //    await _episodeService.AddEpisodeAsync(_episode);
            //}

            //foreach (var lo in location)
            //{
            //    Location _location = new Location()
            //    {
            //        created = lo.created,
            //        dimension = lo.dimension,
            //        name = lo.name,
            //        type = lo.type,
            //        url = lo.url

            //    };
            //    await _locationService.AddLocationAsync(_location);
            //}

            return Ok();
        }

    }
}
