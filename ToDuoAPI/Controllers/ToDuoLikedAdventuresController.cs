using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoLikedAdventuresController : ControllerBase
    {
        private readonly ToDuoDbContext _context;
        private readonly ILikedAdventures _toDuoLikedAdventures;
        private readonly IMapper _mapper;

        public ToDuoLikedAdventuresController(ToDuoDbContext context, ILikedAdventures toDuoLikedAdventures, IMapper mapper)
        {
            _context = context;
            _toDuoLikedAdventures = toDuoLikedAdventures;
            _mapper = mapper;
        }

        // GET: api/ToDuoLikedAdventures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoLikedAdventures>>> GetToDuoLikedAdventures()
        {
            return await _context.ToDuoLikedAdventures.ToListAsync();
        }

        // GET: api/ToDuoLikedAdventures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoLikedAdventures>> GetToDuoLikedAdventures(int id)
        {
            var toDuoLikedAdventures = await _context.ToDuoLikedAdventures.FindAsync(id);

            if (toDuoLikedAdventures == null)
            {
                return NotFound();
            }

            return toDuoLikedAdventures;
        }

        // PUT: api/ToDuoLikedAdventures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDuoLikedAdventures(int id, ToDuoLikedAdventures toDuoLikedAdventures)
        {
            if (id != toDuoLikedAdventures.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDuoLikedAdventures).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDuoLikedAdventuresExists(id))
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

        // POST: api/ToDuoLikedAdventures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<MatchUserDTO>>> PostToDuoLikedAdventures(AdventureBasicUserMatchDTO adventureBasicUserMatchDTO)
        {
            List<MatchUserDTO> matchUserDTOs = new List<MatchUserDTO>();
            bool exists = await _toDuoLikedAdventures.Exists(adventureBasicUserMatchDTO.AdventureID, adventureBasicUserMatchDTO.OwnerID);
            if(!exists)
            {
                ToDuoLikedAdventures toDuoLikedAdventures = new ToDuoLikedAdventures()
                {
                    Id = 0,
                    AdventureID = adventureBasicUserMatchDTO.AdventureID,
                    OwnerID = adventureBasicUserMatchDTO.OwnerID
                };
                await _toDuoLikedAdventures.AddAsync(toDuoLikedAdventures);
                ToDuoBasicUsersDTO basicUsersDTO = _mapper.Map<ToDuoBasicUsersDTO>(adventureBasicUserMatchDTO);
                basicUsersDTO.Id = adventureBasicUserMatchDTO.UserId;
                matchUserDTOs =  await _toDuoLikedAdventures.CheckAdventureMatches(toDuoLikedAdventures.AdventureID, basicUsersDTO);
            }

            return matchUserDTOs;


            
        }

        // DELETE: api/ToDuoLikedAdventures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoLikedAdventures(int id)
        {
            var toDuoLikedAdventures = await _context.ToDuoLikedAdventures.FindAsync(id);
            if (toDuoLikedAdventures == null)
            {
                return NotFound();
            }

            _context.ToDuoLikedAdventures.Remove(toDuoLikedAdventures);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDuoLikedAdventuresExists(int id)
        {
            return _context.ToDuoLikedAdventures.Any(e => e.Id == id);
        }
    }
}
