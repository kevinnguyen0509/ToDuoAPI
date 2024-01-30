using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDuoAPI.Contracts;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Repository
{

    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ToDuoUsers> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ToDuoUsers> userManger, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManger;
            _configuration = configuration;

        }

        public async Task<AuthResponseDTO> Login(ApiUserDTO userDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userDTO.Email);
                var validPassword = await _userManager.CheckPasswordAsync(user, userDTO.Password);
                if (!validPassword || user == null)
                {
                    return null;                 
                }

                var token = await GenerateToken(user);
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = user.Id
                };
            }
            catch (Exception ex)
            {

            }
            return null;
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

        private async Task<string> GenerateToken(ToDuoUsers user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
                ["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim> 
            { 
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
