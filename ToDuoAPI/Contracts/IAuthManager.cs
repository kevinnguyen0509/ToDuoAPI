using Microsoft.AspNetCore.Identity;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDTO userDTO);
        Task<AuthResponseDTO> Login(ApiUserDTO userDTO);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);
    }
}
