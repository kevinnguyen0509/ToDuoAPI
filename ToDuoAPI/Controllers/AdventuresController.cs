using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;
using ToDuoAPI.Service;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdventuresController : ControllerBase
    {
        private readonly ToDuoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAdventures _adventures;

        public AdventuresController(ToDuoDbContext context, IMapper mapper, IAdventures adventures)
        {
            _context = context;
            this._mapper = mapper;
            this._adventures = adventures;
        }

        // GET: api/Adventures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdventureDto>>> GetAdventures()
        {
            var adventures = await _adventures.GetAdventures(300);
            ArrayListHelper.Shuffle(adventures);
            return adventures;
        }

        // GET: api/Adventures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adventures>> GetAdventures(int id)
        {
            var adventures = await _adventures.GetAsync(id);

            if (adventures == null)
            {
                return NotFound();
            }

            return adventures;
        }

        // PUT: api/Adventures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdventures(int id, Adventures adventures)
        {
            if (id != adventures.Id)
            {
                return BadRequest();
            }
            var adventure = await _adventures.GetAsync(id);
            //_context.Entry(adventures).State = EntityState.Modified;
            if(adventure == null)
            {
                return NotFound();
            }

            try
            {
                await _adventures.UpdateAsync(adventures);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdventuresExists(id))
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

        // POST: api/Adventures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Adventures>> PostAdventures(Adventures adventures)
        {
            bool adventureExists = await _adventures.Exists(adventures.Address);
            if (!adventureExists)
            {
                await _adventures.AddAsync(adventures);
            }

            return adventures;
        }


        // POST: api/Adventures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<IEnumerable<Adventures>>> FilterSearch(FilterDto filterDto)
        {
            var adventures = await _adventures.FilteredSearch(filterDto, 300);
            return Ok(adventures);

        }


        // DELETE: api/Adventures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdventures(int id)
        {
            var adventures = await _adventures.GetAsync(id);
            if (adventures == null)
            {
                return NotFound();
            }

            await _adventures.DeleteAsync(id);
            return NoContent();
        }

        private bool AdventuresExists(int id)
        {
            return _context.Adventures.Any(e => e.Id == id);
        }
    }
}
