using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole 
            {
                Id = "11bddaad-739a-4cbe-9b31-72d77622c5df",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },    
            new IdentityRole 
            {
                Id = "29623920-e2f4-4738-ac01-c2ec3c3bdc41",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR"
            },    
            new IdentityRole 
            {
                Id = "60dc1a77-b586-447d-8a88-7077c250911c",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}