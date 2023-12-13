using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Service;

namespace ToDuoAPI.Repository
{
    public class UserRepository : GenericRepository<ToDuoUsers>, IUser
    {
        private readonly ToDuoDbContext _context;
        public UserRepository(ToDuoDbContext context) : base(context)
        {
        }

        public async Task<ToDuoUsers> SignUpNewUser(ToDuoUsers newUser)
        {
            Cryptography cryptography = new Cryptography();
            newUser.Salt = cryptography.createSalt();
            newUser.Password = cryptography.CreateHashAndSaltPassword(newUser.Password, newUser.Salt);

            try
            {
                // Fix the lambda to compare the new user's email with existing emails
                ToDuoUsers userExist = await GetUserByEmail(newUser.Email);
                if (userExist != null)
                {
                    newUser.Result = "There is an existing user with that email...";
                    return newUser;
                }
                else
                {
                    ToDuoUsers user = await AddAsync(newUser);
                    if(user.Id != 0)
                    {
                        newUser.Result = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                newUser.Result = ex.Message;
            }
            return newUser;
        }

        public async Task<ToDuoUsers> AuthenticateCredentials(string email, string password)
        {
            Cryptography cryptography = new Cryptography();
            ToDuoUsers user = new ToDuoUsers();
            bool AuthenticationSuccessful = false;
            try
            {
                user = await GetUserByEmail(email);
                if (user == null)
                {
                    user.Result = "Email or Password is incorrect";
                    return user;
                }

                AuthenticationSuccessful = cryptography.VerifyHashedPassword(user.Password, password, user.Salt);
                if (AuthenticationSuccessful)
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

        public async Task<ToDuoUsers> GetUserByEmail(string email)
        {
            return await _context.ToDuoUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
