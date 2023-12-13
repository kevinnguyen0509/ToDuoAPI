using ToDuoAPI.Models;

namespace ToDuoAPI.Contracts
{
    public interface IUser : IGenericRepository<ToDuoUsers>
    {
        Task<ToDuoUsers> SignUpNewUser(ToDuoUsers newUser);
        Task<ToDuoUsers> GetUserByEmail(string email);
        Task<ToDuoUsers> AuthenticateCredentials(string email, string password);
    }
}
