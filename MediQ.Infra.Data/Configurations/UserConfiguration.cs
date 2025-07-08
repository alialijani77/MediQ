using MediQ.Domain.Entities.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediQ.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var user = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                IsDelete = false,
                Id = "b8819f0a-9e6d-4830-8f06-43efa5069e25",
                UserName = "TestUser@gmail.com",
                Email = "TestUser@gmail.com",
                EmailConfirmed = true,
            };

            var passwordHasher = new PasswordHasher<User>();
            passwordHasher.HashPassword(user, "P@ssword 1234");

            builder
                .ToTable("Users")
                .HasData(user);
        }
    }
}
