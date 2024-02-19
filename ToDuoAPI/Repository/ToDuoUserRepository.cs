using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Repository
{
    public class ToDuoUserRepository : GenericRepository<ToDuoUsers>, IUser
    {
        private readonly ToDuoDbContext _context;
        private readonly IMapper _mapper;
        public ToDuoUserRepository(ToDuoDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ToDuoBasicUsersDTO> GetBasicUserById(int id)
        {
            ToDuoUsers users = await _context.ToDuoUsers.FirstOrDefaultAsync(db => db.Id == id);
            ToDuoBasicUsersDTO userDTO = _mapper.Map<ToDuoBasicUsersDTO>(users);
            if (userDTO == null)
            {
                return null;
            }
            return userDTO;
        }

        public async Task<ToDuoUsers> GetUserByEmail(string email)
        {
            ToDuoUsers users = await _context.ToDuoUsers.FirstOrDefaultAsync(db => db.Email == email);
            if (users == null)
            {
                return null;
            }

            return users;
        }
    }
}
