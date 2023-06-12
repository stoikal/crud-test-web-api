using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
  private readonly DataContext _context;

  public UsersController(DataContext context)
  {
      _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
      return await _context.Users.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<User >> GetUser(string id)
  {

      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
          return NotFound();
      }

      return user;
  }

  // POST: api/User
  [HttpPost]
  public async Task<ActionResult<User>> CreateUser(User user)
  {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUser), new { id = user.userid }, user);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(int id, User user)
  {
      if (id != user.userid)
      {
          return BadRequest();
      }

      _context.Entry(user).State = EntityState.Modified;

      try
      {
          await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
          if (!UserExists(id))
          {
              return NotFound();
          }
          else
          {
              throw;
          }
      }

      return NoContent();
  }

  private bool UserExists(int id)
  {
      return _context.Users.Any(e => e.userid == id);
  }
}
