using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundSesh.Venues.Controllers.Resources;
using SoundSesh.Venues.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Venues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        //private readonly StudioContext _context;
        //private readonly IMapper mapper;

        //public UserAccountController(IMapper mapper, StudioContext context) {
        //    _context = context;
        //    this.mapper = mapper;
        //}

        //[HttpGet]
        //public async Task<IEnumerable<UserAccountResource>> GetUserAccount(UserAccountResource userAccountResource) {
        //    var users = await _context.UserAccount.ToListAsync();
        //    return mapper.Map<IEnumerable<UserAccount>, IEnumerable<UserAccountResource>>(users);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateUserAccount([FromBody] UserAccountResource userAccountResource)
        //{
        //    if(!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    var user = mapper.Map<UserAccountResource, UserAccount>(userAccountResource);
        //    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
        //    _context.UserAccount.Add(user);
        //    await _context.SaveChangesAsync();
        //    return Ok(user);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUserAccount(int id, [FromBody] UserAccountResource userAccountResource) {
        //    if(!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }
        //    var user = await _context.UserAccount.FindAsync(id);
        //    if(user == null) {
        //        return NotFound();
        //    }

        //    mapper.Map<UserAccountResource, UserAccount>(userAccountResource, user);
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserAccount(int id) {
        //    var user = await _context.UserAccount.FindAsync(id);
        //    if(user == null) {
        //        return NotFound();
        //    }            

        //    var userResource = mapper.Map<UserAccount, UserAccountResource>(user);
        //    return Ok(userResource);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserAccount(int id) {
        //    var user = await _context.UserAccount.FindAsync(id);
        //    if(user == null) {
        //        return NotFound();
        //    }
        //    _context.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return Ok(id);
        //}
    }
}