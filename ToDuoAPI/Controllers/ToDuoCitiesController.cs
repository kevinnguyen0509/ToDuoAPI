using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoCitiesController : ControllerBase
    {
        private readonly ToDuoDbContext _context;

        public ToDuoCitiesController(ToDuoDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDuoCities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoCity>>> GetToDuoCity()
        {
            return await _context.ToDuoCity.ToListAsync();
        }

        // GET: api/ToDuoCities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoCity>> GetToDuoCity(int id)
        {
            var toDuoCity = await _context.ToDuoCity.FindAsync(id);

            if (toDuoCity == null)
            {
                return NotFound();
            }

            return toDuoCity;
        }

        // PUT: api/ToDuoCities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDuoCity(int id, ToDuoCity toDuoCity)
        {
            if (id != toDuoCity.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDuoCity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDuoCityExists(id))
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

        // POST: api/ToDuoCities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDuoCity>> PostToDuoCity(ToDuoCity toDuoCity)
        {
            _context.ToDuoCity.Add(toDuoCity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDuoCity", new { id = toDuoCity.Id }, toDuoCity);
        }

        // DELETE: api/ToDuoCities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoCity(int id)
        {
            var toDuoCity = await _context.ToDuoCity.FindAsync(id);
            if (toDuoCity == null)
            {
                return NotFound();
            }

            _context.ToDuoCity.Remove(toDuoCity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDuoCityExists(int id)
        {
            return _context.ToDuoCity.Any(e => e.Id == id);
        }
    }
}
