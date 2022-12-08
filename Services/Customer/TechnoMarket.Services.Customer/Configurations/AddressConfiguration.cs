using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(c => c.Id).HasColumnType("uuid");
            //One to one ilişki
            builder.HasOne(x=> x.Customer).WithOne(x => x.Address).HasForeignKey<Address>(x=> x.CustomerId);
        }
    }
}
