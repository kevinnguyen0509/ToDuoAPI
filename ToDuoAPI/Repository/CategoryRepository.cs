using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;

namespace ToDuoAPI.Repository
{
    public class CategoryRepository : GenericRepository<ToDuoCategory>, ICategory
    {
        private readonly ToDuoDbContext _context;
        public CategoryRepository(ToDuoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
