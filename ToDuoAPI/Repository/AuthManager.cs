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
        private ToDuoUsers _user;

        private const string _loginProvider = "ToDuoAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<ToDuoUsers> userManger, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManger;
            _configuration = configuration;

        }

        public async Task<string> CreateRefreshToken()
        {

            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDTO> Login(ApiUserDTO userDTO)
        {
            try
            {
                _user = await _userManager.FindByEmailAsync(userDTO.Email);
                var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
                if (!validPassword || _user == null)
                {
                    return null;                 
                }

                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userDTO)
        {
            _user = _mapper.Map<ToDuoUsers>(userDTO);
            _user.UserName = _user.Email;
            var result = await _userManager.CreateAsync(_user, userDTO.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByEmailAsync(username);
            if (_user == null) {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> GenerateToken()
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
                ["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim> 
            { 
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
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
