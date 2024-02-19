using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Contracts
{
    public interface IUser : IGenericRepository<ToDuoUsers>
    {
        Task<ToDuoUsers> GetUserByEmail(string email);
        Task<ToDuoBasicUsersDTO> GetBasicUserById(int id);
    }
}
