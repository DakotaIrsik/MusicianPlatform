using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SoundSesh.Venues.Models;
using SoundSesh.Venues.Controllers.Resources;
using Newtonsoft.Json;  

namespace SoundSesh.Venues.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]    
    public class LookUpController : ControllerBase
    {
       // private readonly StudioContext _context;
       // private readonly IMapper mapper;

       // public LookUpController( IMapper mapper, StudioContext context) {
       //     _context = context;
       //     this.mapper = mapper;
       // }    

       //[HttpGet]
       //public async Task<IEnumerable<Genres>> GetGenres() {
       //     List<LookUp> lookup = await _context.LookUp.Where(g=>g.Category == "Genres").ToListAsync();
       //     List<Genres> genres = new List<Genres>();

       //     foreach( var genre in lookup) {
       //         genres.Add(mapper.Map<LookUp, Genres>(genre));               
       //     }
       //     return genres;
       //}

       // [HttpGet("{id}")]
       // public async Task<IActionResult> GetGenre(int id)
       // {
       //     var genre = await _context.LookUp.FindAsync(id);
       //     if(genre == null)
       //         return NotFound();
       //     var genreResource = mapper.Map<LookUp, Genres>(genre);
       //     return Ok(genreResource);
       // }
    }
}