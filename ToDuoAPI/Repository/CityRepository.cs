using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Repository
{
    public class CityRepository : GenericRepository<ToDuoCity>, ICities
    {
        private readonly ToDuoDbContext _context;
        public CityRepository(ToDuoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
