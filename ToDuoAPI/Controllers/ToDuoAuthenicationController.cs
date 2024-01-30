using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDuoAPI.Contracts;
using ToDuoAPI.Models.DataTransferObjects;
using System.Linq;

namespace ToDuoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDuoAuthenicationController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public ToDuoAuthenicationController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }

        // POST: api/ToDuoAuthenication/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody]ApiUserDTO apiUserDTO)
        {
            var errors = await _authManager.RegisterUser(apiUserDTO);

            if (errors.Any()) 
            { 
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                    return BadRequest(errors);
            }

            return Ok();
        }

        // POST: api/ToDuoAuthenication/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> login([FromBody] ApiUserDTO apiUserDTO)
        {
            var isValidUser = await _authManager.Login(apiUserDTO);

            if (isValidUser == null)
            {
                return Unauthorized();
            }

            return Ok(isValidUser);
        }
    }
}
