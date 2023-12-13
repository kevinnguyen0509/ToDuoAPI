using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Repository
{
    public class StateRepository : GenericRepository<ToDuoStates>, IState
    {
        private readonly ToDuoDbContext _context;
        public StateRepository(ToDuoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
