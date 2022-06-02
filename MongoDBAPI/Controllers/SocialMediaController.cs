using Microsoft.AspNetCore.Mvc;
using MongoDBAPI.Models;
using MongoDBAPI.Repositories;

namespace MongoDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : Controller
    {
        private ISocialMediaCollection db = new SocialMediaCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            return Ok(await db.GetAllSocialMedia());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMediaDetails(string id)
        {
            return Ok(await db.GetSocialMediaById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia([FromBody] SocialMedia socialMedia)
        {
            if (socialMedia == null)
                return BadRequest();
            if(socialMedia.name == string.Empty)
            {
                ModelState.AddModelError("name", "El nombre no debe estar vacío");
            }

            await db.InsertSocialMedia(socialMedia);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSocialMedia([FromBody] SocialMedia socialMedia, string id)
        {
            if (socialMedia == null)
                return BadRequest();
            if (socialMedia.name == string.Empty)
            {
                ModelState.AddModelError("name", "El nombre no debe estar vacío");
            }

            socialMedia.id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateSocialMedia(socialMedia);

            return Created("Created", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSocialMedia(string id)
        {
            await db.DeleteSocialMedia(id);

            return NoContent(); 
        }
    }
}
