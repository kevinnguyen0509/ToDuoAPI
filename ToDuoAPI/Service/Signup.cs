using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Service
{
    public class Signup
    {
        private readonly ToDuoDbContext _context;
        public Signup(ToDuoDbContext context)
        {
            _context = context;
        }
        public async Task<ToDuoUsers> SignUpNewUser(ToDuoUsers newUser)
        {
            Cryptography cryptography = new Cryptography();
            newUser.Salt = cryptography.createSalt();
            newUser.Password = cryptography.CreateHashAndSaltPassword(newUser.Password, newUser.Salt);

            try
            {
                // Fix the lambda to compare the new user's email with existing emails
                ToDuoUsers userExist = await _context.ToDuoUsers.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                if (userExist != null)
                {
                    newUser.Result = "There is an existing user with that email...";
                    return newUser;
                }
                else
                {
                    _context.ToDuoUsers.Add(newUser);
                    await _context.SaveChangesAsync(); // Use SaveChangesAsync and await it
                    newUser.Result = "Success";
                }
            }
            catch (Exception ex)
            {
                newUser.Result = ex.Message;
            }
            return newUser;
        }
    }
}
