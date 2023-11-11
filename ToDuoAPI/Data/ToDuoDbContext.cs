using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Models;

namespace ToDuoAPI.Data
{
    public class ToDuoDbContext : DbContext
    {
        public ToDuoDbContext(DbContextOptions options) : base(options)
        {

        }  

        public DbSet<ToDuoUsers> ToDuoUsers { get; set; }
    }
}
