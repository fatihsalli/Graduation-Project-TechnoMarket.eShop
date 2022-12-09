using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Configurations.PostgreSql;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            //Id-uuidv4
            builder.Property(c => c.Id)
                .HasColumnType(ColumnTypes.UniqueIdentifier)
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
