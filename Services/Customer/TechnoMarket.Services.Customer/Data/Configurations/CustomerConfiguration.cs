using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Configurations.PostgreSql; //ColumnTypesları kullanmak için => timestamp de hata veriyor.

namespace TechnoMarket.Services.Customer.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnType(ColumnTypes.Varchar)
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(55);

            builder.Property(c => c.Email)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CreatedAt)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("timestamp");
        }
    }
}
