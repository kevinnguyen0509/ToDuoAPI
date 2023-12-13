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
    public class ToDuoStatesController : ControllerBase
    {
        private readonly ToDuoDbContext _context;
        private readonly IState _state;

        public ToDuoStatesController(ToDuoDbContext context, IState state)
        {
            _context = context;
            _state = state;
        }

        // GET: api/ToDuoStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoStates>>> GetToDuoStates()
        {
            return await _state.GetAllAsync();
        }

        // GET: api/ToDuoStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoStates>> GetToDuoStates(int id)
        {
            var toDuoStates = await _state.GetAsync(id);
            if (toDuoStates == null)
            {
                return NotFound();
            }

            return toDuoStates;
        }

        // PUT: api/ToDuoStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDuoStates(int id, ToDuoStates toDuoStates)
        {
            if (id != toDuoStates.Id)
            {
                return BadRequest();
            }

            bool stateExists = await _state.Exists(id);
            if (!stateExists)
            {
                return NotFound();
            }

            try
            {
                await _state.UpdateAsync(toDuoStates);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDuoStatesExists(id))
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

        // POST: api/ToDuoStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDuoStates>> PostToDuoStates(ToDuoStates toDuoStates)
        {
            await _state.AddAsync(toDuoStates);
            return CreatedAtAction("GetToDuoStates", new { id = toDuoStates.Id }, toDuoStates);
        }

        // DELETE: api/ToDuoStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoStates(int id)
        {
            var toDuoStates = await _state.GetAsync(id);
            if (toDuoStates == null)
            {
                return NotFound();
            }

            await _state.DeleteAsync(id);
            return NoContent();
        }

        private bool ToDuoStatesExists(int id)
        {
            return _context.ToDuoStates.Any(e => e.Id == id);
        }
    }
}
