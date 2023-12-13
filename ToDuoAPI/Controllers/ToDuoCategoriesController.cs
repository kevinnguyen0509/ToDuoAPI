using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoCategoriesController : ControllerBase
    {
        private readonly ToDuoDbContext _context;
        private readonly ICategory _category;

        public ToDuoCategoriesController(ToDuoDbContext context, ICategory category)
        {
            _context = context;
            _category = category;
        }

        // GET: api/ToDuoCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoCategory>>> GetToDuoCategories()
        {
            return await _category.GetAllAsync();
        }

        // GET: api/ToDuoCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoCategory>> GetToDuoCategory(int id)
        {
            bool categoryExists = await _category.Exists(id);    

            if (!categoryExists)
            {
                return NotFound();
            }
            var toDuoCategory = await _category.GetAsync(id);
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

            var category = await _category.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                await _category.UpdateAsync(category);
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
            await _category.AddAsync(toDuoCategory);
            return CreatedAtAction("GetToDuoCategory", new { id = toDuoCategory.Id }, toDuoCategory);
        }

        // DELETE: api/ToDuoCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoCategory(int id)
        {

            bool categoryExists = await _category.Exists(id);
            if (!categoryExists)
            {
                return NotFound();
            }

            await _category.DeleteAsync(id);
            return NoContent();
        }

        private bool ToDuoCategoryExists(int id)
        {
            return _context.ToDuoCategories.Any(e => e.Id == id);
        }
    }
}
