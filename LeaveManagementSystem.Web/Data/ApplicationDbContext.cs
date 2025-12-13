using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
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

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
            
                new ApplicationUser
                { 
                    Id = "e9f7bf52-b796-43e1-8771-3384194ab576",
                    Email = "admin@localhost.com",
                    UserName = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true,
                    DateOfBirth = new DateOnly(1995, 03, 15),
                    FirstName = "Default",
                    LastName = "Admin"
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
            
                new IdentityUserRole<string>
                { 
                    RoleId = "60dc1a77-b586-447d-8a88-7077c250911c",
                    UserId = "e9f7bf52-b796-43e1-8771-3384194ab576"
                }
            );
        }
        
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation>  LeaveAllocations { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
