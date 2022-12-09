using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnoMarket.Services.Customer.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            //Id
            builder.Property(c => c.Id).HasColumnType("varchar").HasMaxLength(36);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
            builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("timestamp");
            builder.Property(c => c.UpdatedAt).IsRequired(false).HasColumnType("timestamp");

        }
    }
}
