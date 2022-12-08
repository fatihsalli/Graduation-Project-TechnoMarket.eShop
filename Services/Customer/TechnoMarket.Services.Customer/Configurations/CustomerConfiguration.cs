using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            builder.Property(c=> c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(255);

        }
    }
}
