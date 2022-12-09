using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Customer.Models;
using Threenine.Configurations.PostgreSql;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //Id-Uuid
            builder.Property(c => c.Id)
                .HasColumnType(ColumnTypes.UniqueIdentifier)
                .IsRequired();

            builder.Property(c => c.CustomerId)
                .HasColumnType(ColumnTypes.UniqueIdentifier)
                .IsRequired();

            builder.Property(c => c.AddressLine)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(c => c.City)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(55);

            builder.Property(c => c.Country)
                .HasColumnType(ColumnTypes.Varchar)
                .IsRequired()
                .HasMaxLength(55);

            builder.Property(c => c.CityCode)
                .HasColumnType(ColumnTypes.SmallInt)
                .IsRequired()
                .HasMaxLength(81);

            //One to one ilişki
            builder.HasOne(x=> x.Customer).WithOne(x => x.Address).HasForeignKey<Address>(x=> x.CustomerId);
        }
    }
}
