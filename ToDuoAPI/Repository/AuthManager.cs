using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDuoAPI.Contracts;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Repository
{

    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ToDuoUsers> _userManager;

        public AuthManager(IMapper mapper, UserManager<ToDuoUsers> userManger)
        {
            this._mapper = mapper;
            this._userManager = userManger;
        }
        public async Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userDTO)
        {
            var user = _mapper.Map<ToDuoUsers>(userDTO);
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}
