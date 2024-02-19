using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Data.Configurations;
using ToDuoAPI.Models;

namespace ToDuoAPI.Data
{
    public class ToDuoDbContext : IdentityDbContext<ToDuoUsers, IdentityRole<int>, int>
    {
        public ToDuoDbContext(DbContextOptions options) : base(options)
        {

        }  

        public DbSet<ToDuoUsers> ToDuoUsers { get; set; }
        public DbSet<Adventures> Adventures { get; set; }
        public DbSet<ToDuoCategory> ToDuoCategories { get; set; }
        public DbSet<ToDuoStates> ToDuoStates { get; set; }
        public DbSet<ToDuoLikedAdventures> ToDuoLikedAdventures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
