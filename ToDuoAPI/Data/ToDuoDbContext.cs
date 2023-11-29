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
        public DbSet<Adventures> Adventures { get; set; }
        public DbSet<ToDuoCategory> ToDuoCategories { get; set; }
        public DbSet<ToDuoStates> ToDuoStates { get; set; }
        public DbSet<ToDuoCity> ToDuoCity { get; set; }
    }
}
