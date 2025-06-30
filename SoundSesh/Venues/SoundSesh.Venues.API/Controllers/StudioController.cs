using GenericBizRunner;
using GenericServices.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SoundSesh.Venues.Controllers.Resources;
using SoundSesh.Venues.Core.BusinessLogic;
using SoundSesh.Venues.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Create = SoundSesh.Venues.Entities.DTOs.Create;

namespace SoundSesh.Venues.Controllers
{

    [Route("api/[controller]")]     
    [ApiController]     
    public class StudioController : ControllerBase     
    {        
        public StudioController()         
        {

        }            
        
        [HttpGet(Name="GetStudio")]
        public async Task<IEnumerable<StudioResource>> GetStudio(StudioResource studioResource)
        {
            //TODO
            return new List<StudioResource>();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Studio), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        public ActionResult<Studio> CreateStudio([FromServices] IActionService<ICreateStudio> studio, [FromBody]Create.StudioDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = studio.RunBizAction<Studio>(inputData);

            return studio.Status.Response(this, "GetStudio", new { id = inputData?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudio(int id, [FromBody] StudioResource studioResource)
        {
            //TODO
            return new JsonResult(0);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudio(int id)
        {
            return new JsonResult(0);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(int id)
        {
            //TODO
            return new JsonResult(0);
        }
    } 
}

