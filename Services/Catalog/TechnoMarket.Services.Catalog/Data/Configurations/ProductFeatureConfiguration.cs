using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Data.Configurations
{
    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Height)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Width)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Weight)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
