using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SoundSesh.Venues.Models;
using SoundSesh.Venues.Controllers.Resources;
using Newtonsoft.Json;
using BCrypt.Net;

namespace SoundSesh.Venues.Controllers {     

    [Route("api/[controller]")]     
    [ApiController]     
    public class StudioStagingController : ControllerBase     
    {        
       // private readonly StudioContext _context;  
       // private readonly IMapper mapper;        
       // public StudioStagingController(IMapper mapper, StudioContext context)         
       // {   _context = context;
       //     this.mapper = mapper;
       // }            

       // [HttpGet]
       // public async Task<IEnumerable<StudioResource>> GetStudio(StudioResource studioResource)
       // {
       //     var studio = await _context.Studio.ToListAsync();
       //     return mapper.Map<IEnumerable<Studio>, IEnumerable<StudioResource>>(studio);
       // } 

       // [HttpPost]
       // public async Task<IActionResult> CreateStudio([FromBody] StudioResource studioResource)
       // {
       //     if(!ModelState.IsValid)
       //         return BadRequest(ModelState);
       //     var studio = mapper.Map<StudioResource, StudioStaging>(studioResource);
       //     studio.Password = BCrypt.Net.BCrypt.HashPassword(studio.Password);
       //     _context.StudioStaging.Add(studio);
       //     await _context.SaveChangesAsync();
       //     return Ok(studio);
       // }

       // [HttpPut("{id}")]
       // public async Task<IActionResult> UpdateStudio(int id, [FromBody] StudioResource studioResource)
       // {
            
       //     if(!ModelState.IsValid)
       //         return BadRequest(ModelState);
       //     var studio = await _context.StudioStaging.FindAsync(id); 
       //     if(studio == null)
       //         return NotFound();

       //     // Create/Update Schedule on Staging Table
       //     if( studioResource.BusinessHours != null && studio.StudioScheduleId != 0) {
       //         await UpdateStudioSchedule(studioResource.BusinessHours, studio.StudioScheduleId);                       
       //     } else if (studioResource.BusinessHours != null && studio.StudioScheduleId == 0) {
       //         var actionresult = await CreateStudioSchedule(studioResource.BusinessHours);
       //         studioResource.StudioScheduleId = actionresult.Value;
       //     }

       //     // If rooms added, store them in JSON object to send to database
       //     studio.Rooms = studioResource.Rooms;
       //     if( studio.Rooms != null && studio.Rooms.Count > 0) {
       //         string roomDetails = JsonConvert.SerializeObject(studio.Rooms);
       //         System.Console.WriteLine(roomDetails);
       //         studioResource.RoomDetails = roomDetails;
       //     }

       //     // If Genres added, store them in JSON object to send to database
       //     if( studioResource.Genres != null && studioResource.Genres.Count > 0 ) {
       //         string genreDetails = JsonConvert.SerializeObject(studioResource.Genres);
       //         System.Console.WriteLine(genreDetails);
       //         studioResource.GenreDetails = genreDetails;
       //     }

       //     mapper.Map<StudioResource, StudioStaging>(studioResource, studio);
       //     //mapper.Map<StudioStaging, StudioResource>(studio, studioResource);

       //     //_context.StudioStaging.Update(studio);

       //     await _context.SaveChangesAsync();
       
       //     return Ok(studio);
       // }
 
       // [HttpGet("{id}")]
       // public async Task<IActionResult> GetStudio(int id)
       // {
       //     var studio = await _context.StudioStaging.FindAsync(id);
       //     if(studio == null)
       //         return NotFound();
       //     var studioResource = mapper.Map<StudioStaging, StudioResource>(studio);
       //     return Ok(studioResource);
       // }

       // [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteStudio(int id)
       // {
       //     var studio = await _context.StudioStaging.FindAsync(id);
       //     if(studio == null)
       //         return NotFound();
       //     _context.Remove(studio);
       //     await _context.SaveChangesAsync();
       //     return Ok(id);
       // }

       // [HttpPost]
       // private async Task<ActionResult<int>> CreateStudioSchedule(BusinessHours businessHours) {
       //     StudioScheduleResource studioScheduleResource = new StudioScheduleResource();
       //     setScheduleValues(studioScheduleResource, businessHours);

       //     if(!ModelState.IsValid)
       //         return BadRequest(ModelState);
       //     var studioSchedule = mapper.Map<StudioScheduleResource, StudioSchedule>(studioScheduleResource);
       //     _context.StudioSchedule.Add(studioSchedule);
       //     await _context.SaveChangesAsync();
       //     return studioSchedule.Id;
       //}

       // [HttpPut("{id}")]
       // private async Task<ActionResult<int>> UpdateStudioSchedule(BusinessHours businessHours, int id) {
       //     StudioScheduleResource studioScheduleResource = new StudioScheduleResource();
       //     setScheduleValues(studioScheduleResource, businessHours);

       //     if(!ModelState.IsValid)
       //         return BadRequest(ModelState);
       //     var studioSchedule = await _context.StudioSchedule.FindAsync(id); 
       //     if(studioSchedule == null)
       //         return NotFound();
       //     mapper.Map<StudioScheduleResource, StudioSchedule>(studioScheduleResource, studioSchedule);
       //     await _context.SaveChangesAsync();
       //     return NoContent();
       //}

       //private void setScheduleValues(StudioScheduleResource studioScheduleResource, BusinessHours businessHours) {
       //     studioScheduleResource.MondayIsOpen = businessHours.monday.IsOpen;
       //     studioScheduleResource.MondayOpenTime = businessHours.monday.OpenTime;
       //     studioScheduleResource.MondayCloseTime = businessHours.monday.CloseTime;
       //     studioScheduleResource.TuesdayIsOpen = businessHours.tuesday.IsOpen;
       //     studioScheduleResource.TuesdayOpenTime = businessHours.tuesday.OpenTime;
       //     studioScheduleResource.TuesdayCloseTime = businessHours.tuesday.CloseTime;
       //     studioScheduleResource.WednesdayIsOpen = businessHours.wednesday.IsOpen;
       //     studioScheduleResource.WednesdayOpenTime = businessHours.wednesday.OpenTime;
       //     studioScheduleResource.WednesdayCloseTime = businessHours.wednesday.CloseTime;
       //     studioScheduleResource.ThursdayIsOpen = businessHours.thursday.IsOpen;
       //     studioScheduleResource.ThursdayOpenTime = businessHours.thursday.OpenTime;
       //     studioScheduleResource.ThursdayCloseTime = businessHours.thursday.CloseTime;
       //     studioScheduleResource.FridayIsOpen = businessHours.friday.IsOpen;
       //     studioScheduleResource.FridayOpenTime = businessHours.friday.OpenTime;
       //     studioScheduleResource.FridayCloseTime = businessHours.friday.CloseTime;
       //     studioScheduleResource.SaturdayIsOpen = businessHours.saturday.IsOpen;
       //     studioScheduleResource.SaturdayOpenTime = businessHours.saturday.OpenTime;
       //     studioScheduleResource.SaturdayCloseTime = businessHours.saturday.CloseTime;
       //     studioScheduleResource.SundayIsOpen = businessHours.sunday.IsOpen;
       //     studioScheduleResource.SundayOpenTime = businessHours.sunday.OpenTime;
       //     studioScheduleResource.SundayCloseTime = businessHours.sunday.CloseTime;
       //}
    } 
}