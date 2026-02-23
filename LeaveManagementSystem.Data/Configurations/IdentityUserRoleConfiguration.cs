using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            
            new IdentityUserRole<string>
            { 
                RoleId = "60dc1a77-b586-447d-8a88-7077c250911c",
                UserId = "e9f7bf52-b796-43e1-8771-3384194ab576"
            }
        );
    }
}