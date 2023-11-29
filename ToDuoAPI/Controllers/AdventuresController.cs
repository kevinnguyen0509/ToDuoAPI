﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;
using ToDuoAPI.Service;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventuresController : ControllerBase
    {
        private readonly ToDuoDbContext _context;

        public AdventuresController(ToDuoDbContext context)
        {
            _context = context;
        }

        // GET: api/Adventures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdventureDto>>> GetAdventures()
        {
            var adventures = await _context.Adventures
            .Include(a => a.ToDuoCategory)
            .Select(a => new AdventureDto
            {
                Id = a.Id,
                OwnerId = a.OwnerId,
                Title = a.Title,
                ImageURL = a.ImageURL,
                Description = a.Description,
                WebsiteURL = a.WebsiteURL,
                CreatedDate = a.CreatedDate,
                Address = a.Address,
                City = a.ToDuoCity.Name,
                State = a.ToDuoStates.Name,
                Tags = a.Tags,
                Hours = a.Hours,
                SwipeCount = a.SwipeCount,
                CategoryName = a.ToDuoCategory.Name // Only the category name
            })
            .OrderByDescending(db => db.Id)
            .Take(200)
            .ToListAsync();

            ArrayListHelper.Shuffle(adventures);
            return adventures;
            //return await _context.Adventures.Include(db => db.ToDuoCategory).ToListAsync();
        }

        // GET: api/Adventures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adventures>> GetAdventures(int id)
        {
            var adventures = await _context.Adventures.FindAsync(id);

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

            _context.Entry(adventures).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            Adventures adventure = _context.Adventures.FirstOrDefault(db => db.Address == adventures.Address);
            if (adventure == null)
            {
                _context.Adventures.Add(adventures);
                await _context.SaveChangesAsync();
            }


            return adventures;
        }

        // DELETE: api/Adventures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdventures(int id)
        {
            var adventures = await _context.Adventures.FindAsync(id);
            if (adventures == null)
            {
                return NotFound();
            }

            _context.Adventures.Remove(adventures);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdventuresExists(int id)
        {
            return _context.Adventures.Any(e => e.Id == id);
        }
    }
}
