using AngularAuthTest.Server.Data;
using AngularAuthTest.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthTest.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly Context _context;

    public UserController(Context context)
    {
        _context = context;
    }
    
    // GET: api/user/
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    
    // GET: api/user/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();
        return user;
    }
    // PUT: api/user/5
    [HttpPut("{id}")]//только для обновления
    public async Task<IActionResult> PutUser(int id, User user)//user должен быть с новыми данными
    {
        if (id != user.Id)
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
            throw;
        }
        return NoContent();
    }
    // POST: api/user
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}