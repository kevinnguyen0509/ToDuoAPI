using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Service;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoUsersController : ControllerBase
    {
        private readonly ToDuoDbContext _context;
        private readonly Signup _signup;

        public ToDuoUsersController(ToDuoDbContext context, Signup signup)
        {
            _context = context;
            _signup = signup;
        }

        // GET: api/ToDuoUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoUsers>>> GetToDuoUsers()
        {
            return await _context.ToDuoUsers.ToListAsync();
        }

        // GET: api/ToDuoUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoUsers>> GetToDuoUsers(int id)
        {
            var toDuoUsers = await _context.ToDuoUsers.FindAsync(id);

            if (toDuoUsers == null)
            {
                return NotFound();
            }

            return toDuoUsers;
        }

        // PUT: api/ToDuoUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDuoUsers(int id, ToDuoUsers toDuoUsers)
        {
            if (id != toDuoUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDuoUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDuoUsersExists(id))
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

        // POST: api/ToDuoUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDuoUsers>> PostToDuoUsers(ToDuoUsers toDuoUsers)
        {
            toDuoUsers =  await _signup.SignUpNewUser(toDuoUsers);
            return CreatedAtAction("GetToDuoUsers", new { id = toDuoUsers.Id }, toDuoUsers);
        }

        // DELETE: api/ToDuoUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoUsers(int id)
        {
            var toDuoUsers = await _context.ToDuoUsers.FindAsync(id);
            if (toDuoUsers == null)
            {
                return NotFound();
            }

            _context.ToDuoUsers.Remove(toDuoUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDuoUsersExists(int id)
        {
            return _context.ToDuoUsers.Any(e => e.Id == id);
        }
    }
}
