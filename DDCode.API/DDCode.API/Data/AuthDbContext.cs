using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DDCode.API.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "6a31b8ff-cea7-4c69-8873-399331ebba7d";
            var writerRoleId = "47c3e4b5-4ce5-49f9-9b99-6d8cf9b6b6ea";

            var roles = new[]
            {
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                },
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            var adminId = "e65ef4d9-9bf4-4139-91f8-ab6474ee2b93";
            var adminData = "admin@ddcode.com";

            var admin = new IdentityUser
            { 
                Id = adminId,
                UserName= adminData,
                Email = adminData,
                NormalizedEmail = adminData.ToUpper(),
                NormalizedUserName = adminData.ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin1234!");

            builder.Entity<IdentityUser>().HasData(admin);
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = adminId,
                    RoleId = writerRoleId
                },
                new()
                {
                    UserId = adminId,
                    RoleId = readerRoleId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);



        }
    }
    
    
}
