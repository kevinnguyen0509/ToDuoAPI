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
using ToDuoAPI.Models.DataTransferObjects;
using ToDuoAPI.Service;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoUsersController : ControllerBase
    {
        private readonly IUser _user;
/*        private readonly Signup _signup;
        private readonly LoginService _login;*/

        public ToDuoUsersController(ToDuoDbContext context, IUser user)
        {
            _user = user;
        }

        // GET: api/ToDuoUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDuoUsers>>> GetToDuoUsers()
        {
            return await _user.GetAllAsync();
        }

        // GET: api/ToDuoUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDuoBasicUsersDTO>> GetToDuoUsers(int id)
        {
            var toDuoUsers = await _user.GetBasicUserById(id);

            if (toDuoUsers == null)
            {
                return NotFound();
            }

            return toDuoUsers;
        }

        [HttpGet("getfriends/{id}")]
        public async Task<ActionResult<ToDuoUsers>> GetFriends(int id)
        {
            return null;
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

            bool userExists = await _user.Exists(id);
            if (!userExists)
            {
                return NotFound();
            }

            try
            {
                await _user.UpdateAsync(toDuoUsers);
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exists = await _user.Exists(id);
                if (!exists)
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


        // DELETE: api/ToDuoUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDuoUsers(int id)
        {
            var toDuoUsers = await _user.GetAsync(id);
            if (toDuoUsers == null)
            {
                return NotFound();
            }

            await _user.DeleteAsync(id);
            return NoContent();
        }

    }
}
