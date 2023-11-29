using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Service
{
    public class LoginService
    {
        private readonly ToDuoDbContext _context;
        public LoginService(ToDuoDbContext context)
        {
            _context= context;
        }

        public async Task<ToDuoUsers> AuthenticateCredentials(string email, string password)
        {
            Cryptography cryptography = new Cryptography();
            ToDuoUsers user = new ToDuoUsers();
            bool AuthenticationSuccessful = false;
            try
            {
                user = await _context.ToDuoUsers.FirstOrDefaultAsync(db => db.Email == email);
                if(user == null)
                {
                    user.Result = "Email or Password is incorrect";
                    return user;
                }

                AuthenticationSuccessful = cryptography.VerifyHashedPassword(user.Password, password, user.Salt);
                if(AuthenticationSuccessful)
                {
                    user.Result = "Success";
                    return user;
                }
                    
            }
            catch (Exception ex)
            {
                user.Result = ex.Message;
            }
            user.Result = "Email or Password is incorrect";
            return user;
        }
    }
}
