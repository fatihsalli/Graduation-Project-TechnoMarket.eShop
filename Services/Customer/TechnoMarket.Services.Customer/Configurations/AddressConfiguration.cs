using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //Id
            builder.Property(c => c.Id).HasColumnType("varchar").HasMaxLength(36);

            builder.Property(c => c.AddressLine).IsRequired().HasMaxLength(255);
            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Country).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CityCode).IsRequired().HasColumnType("smallint").HasMaxLength(81);

            //One to one ilişki
            builder.HasOne(x=> x.Customer).WithOne(x => x.Address).HasForeignKey<Address>(x=> x.CustomerId);
        }
    }
}
