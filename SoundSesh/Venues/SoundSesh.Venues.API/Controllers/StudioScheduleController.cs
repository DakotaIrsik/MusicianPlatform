using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SoundSesh.Venues.Models;
using SoundSesh.Venues.Controllers.Resources;

namespace SoundSesh.Venues.Controllers
{
    [Route("api/[controller]")]     
    [ApiController]  
    public class StudioScheduleController : ControllerBase 
    {
        //private readonly StudioContext _context;  
        //private readonly IMapper mapper;        
        //public StudioScheduleController(IMapper mapper, StudioContext context)         
        //{   _context = context;
        //    this.mapper = mapper;
        //}            

        //[HttpGet]
        //public async Task<IEnumerable<StudioScheduleResource>> GetStudioSchedule(StudioScheduleResource studioScheduleResource)
        //{
        //    var studioSchedule = await _context.StudioSchedule.ToListAsync();
        //    return mapper.Map<IEnumerable<StudioSchedule>, IEnumerable<StudioScheduleResource>>(studioSchedule);
        //} 

        //[HttpPost]
        //public async Task<IActionResult> CreateStudioSchedule([FromBody] StudioScheduleResource studioScheduleResource)
        //{
        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var studioSchedule = mapper.Map<StudioScheduleResource, StudioStaging>(studioScheduleResource);
        //    _context.StudioStaging.Add(studioSchedule);
        //    await _context.SaveChangesAsync();
        //    return Ok(studioSchedule);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateStudioSchedule(int id, [FromBody] StudioScheduleResource studioScheduleResource)
        //{
        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var studioSchedule = await _context.StudioSchedule.FindAsync(id); 
        //    if(studioSchedule == null)
        //        return NotFound();
        //    mapper.Map<StudioScheduleResource, StudioSchedule>(studioScheduleResource, studioSchedule);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //    //return Ok(studio);
        //}


        
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetStudioSchedule(int id)
        //{
        //    var studioSchedule = await _context.StudioSchedule.FindAsync(id);
        //    if(studioSchedule == null)
        //        return NotFound();
        //    var studioStagingResource = mapper.Map<StudioSchedule, StudioScheduleResource>(studioSchedule);
        //    return Ok(studioStagingResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudioSchedule(int id)
        //{
        //    var studioSchedule = await _context.StudioSchedule.FindAsync(id);
        //    if(studioSchedule == null)
        //        return NotFound();
        //    _context.Remove(studioSchedule);
        //    await _context.SaveChangesAsync();
        //    return Ok(id);
        //}
    } 
}