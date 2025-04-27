using MediQ.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediQ.Infra.Data.Configurations
{
    public class EmailSettingConfiguration : IEntityTypeConfiguration<EmailSetting>
    {
        public void Configure(EntityTypeBuilder<EmailSetting> builder)
        {
            EmailSetting emailSetting = new()
            {
                CreateDate = DateTime.Now,
                DisplayName = "MediQ",
                EnableSSL = true,
                From = "mediqsystem@gmail.com",
                Id = 1,
                IsDefault = true,
                IsDelete = false,
                Password = "tkisthnoqbtvsmlq",
                Port = 587,
                SMTP = "smtp.gmail.com"
            };

            builder
                .ToTable("EmailSettings")
                .HasData(emailSetting);
        }
    }
}
