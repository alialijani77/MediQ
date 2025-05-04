using MediQ.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediQ.Infra.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            List<Role> roles =
            [
                new() { Id = "e06cafc4-898c-4795-af79-c34ddfa69ef0", Name = "Admin" },
                new() { Id = "0e882054-2fcf-4084-983f-61538bd6d8a4", Name = "User" }
            ];

            builder
                .ToTable("Roles")
                .HasData(roles);
        }
    }
}
