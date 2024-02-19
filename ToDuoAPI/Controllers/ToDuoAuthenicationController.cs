using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDuoAPI.Contracts;
using ToDuoAPI.Models.DataTransferObjects;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> Register([FromBody] ApiRegisterUserDTO apiUserDTO)
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

        // POST: api/ToDuoAuthenication/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> refreshtoken([FromBody] AuthResponseDTO request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
