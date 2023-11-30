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
    public class ToDuoCategoriesController : ControllerBase
    {
        private readonly ToDuoDbContext _context;

        public ToDuoCategoriesController(ToDuoDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDuoCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoCategory>>> GetToDuoCategories()
        {
            return await _context.ToDuoCategories.ToListAsync();
        }

        // GET: api/ToDuoCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoCategory>> GetToDuoCategory(int id)
        {
            var toDuoCategory = await _context.ToDuoCategories.FindAsync(id);

            if (toDuoCategory == null)
            {
                return NotFound();
            }

            return toDuoCategory;
        }

        // PUT: api/ToDuoCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDuoCategory(int id, ToDuoCategory toDuoCategory)
        {
            if (id != toDuoCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDuoCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDuoCategoryExists(id))
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

        // POST: api/ToDuoCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDuoCategory>> PostToDuoCategory(ToDuoCategory toDuoCategory)
        {
            _context.ToDuoCategories.Add(toDuoCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDuoCategory", new { id = toDuoCategory.Id }, toDuoCategory);
        }

        // DELETE: api/ToDuoCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoCategory(int id)
        {
            var toDuoCategory = await _context.ToDuoCategories.FindAsync(id);
            if (toDuoCategory == null)
            {
                return NotFound();
            }

            _context.ToDuoCategories.Remove(toDuoCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDuoCategoryExists(int id)
        {
            return _context.ToDuoCategories.Any(e => e.Id == id);
        }
    }
}
