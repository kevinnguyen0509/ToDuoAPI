using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDuoAPI.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
                new IdentityRole<int>
                { 
                    Id = 1,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                },

                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                }
                
           );
        }
    }
}
