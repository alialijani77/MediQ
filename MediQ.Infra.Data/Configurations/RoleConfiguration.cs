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
                new() { Id = 1, Name = "Admin" },
                new() { Id = 2, Name = "User" }
            ];

            builder
                .ToTable("Roles")
                .HasData(roles);
        }
    }
}
