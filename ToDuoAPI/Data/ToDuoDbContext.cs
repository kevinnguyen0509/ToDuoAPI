using Microsoft.EntityFrameworkCore;

namespace ToDuoAPI.Data
{
    public class ToDuoDbContext : DbContext
    {
        public ToDuoDbContext(DbContextOptions options) : base(options)
        {

        }  
    }
}
